using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class OffscreenMarkers : MonoBehaviour
{
    private class ScreenMarkerTracker
    {
        public ScreenMarkerTrackerHandle AssignedHandle;
        public Transform Target;
        public RectTransform Marker;
        public Camera Camera;
        public MarkerSettings MarkerSettings;
        public bool IsReleasedOnceOnScreen = false;
        public RequestRelease RequestRelease;

        public void Assigned(Transform target, bool isReleaseOnceOnScreen)
        {
            Target = target;
            IsReleasedOnceOnScreen = isReleaseOnceOnScreen;
            Marker.gameObject.SetActive(true);
            Update();
        }

        public void Update()
        {
            Vector3 targetPosition = Target.position;
            if (ScreenBounds.GetOffsetBorderPositionForOffscreenObject(targetPosition, Camera, MarkerSettings.PositionOffset, out Vector3 screenEdgePosition))
            {
                UpdateSize(targetPosition);
                UpdateRotation();
                UpdatePosition(screenEdgePosition);
            }
            else if (IsReleasedOnceOnScreen)
            {
                RequestRelease?.Invoke(AssignedHandle);
            }
        }

        private void UpdatePosition(Vector3 position)
        {
            Marker.position = position;
            Vector3 localPosition = Marker.localPosition;
            localPosition.z = 0.0f;
            Marker.localPosition = localPosition;
        }

        private void UpdateSize(Vector3 targetPosition)
        {
            Vector3 edgePosition = GetClosestEdgePosition(targetPosition);
            float squaredDistance = (edgePosition - targetPosition).sqrMagnitude;
        }

        private void UpdateRotation()
        {
            // dont need it this case
        }

        private Vector3 GetClosestEdgePosition(Vector3 targetPosition)
        {
            Vector3 screenPoint = Camera.WorldToScreenPoint(targetPosition);
            screenPoint.x = Mathf.Clamp(screenPoint.x, 0.0f, Screen.width);
            screenPoint.y = Mathf.Clamp(screenPoint.y, 0.0f, Screen.height);
            return Camera.ScreenToWorldPoint(screenPoint);
        }
    }

    protected delegate void RequestRelease(ScreenMarkerTrackerHandle handle);

    static public OffscreenMarkers Instance { get; private set; }

    [SerializeField]
    private RectTransform _markerPrefab;

    [SerializeField]
    private int _poolSize;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private MarkerSettings _settings;

    private ScreenMarkerTracker[] _markers;
    private List<ScreenMarkerTracker> _activeMarkers = new List<ScreenMarkerTracker>();
    private Queue<ScreenMarkerTracker> _availableMarkers = new Queue<ScreenMarkerTracker>();

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            throw new Exception($"There should only be one existingg OffscreenMarkers object. - Instance: {Instance.name} Second object: {name}");
        }
    }

    private void Start()
    {
        _markers = new ScreenMarkerTracker[_poolSize];
        for (int i = 0; i < _poolSize; i++)
        {
            ScreenMarkerTracker markerTracker = new ScreenMarkerTracker();
            RectTransform marker = Instantiate(_markerPrefab, transform);
            marker.gameObject.SetActive(false);

            markerTracker.Marker = marker;
            markerTracker.AssignedHandle = new ScreenMarkerTrackerHandle(i);
            markerTracker.Camera = _camera;
            markerTracker.RequestRelease = ReleaseScreenTracker;
            markerTracker.MarkerSettings = _settings;

            _markers[i] = markerTracker;
            _availableMarkers.Enqueue(markerTracker);
        }
    }

    private void Update()
    {
        // Backwards for loop to be able to remove active markers as they update
        for(int i = _activeMarkers.Count - 1; i >= 0; i--)
        {
            _activeMarkers[i].Update();
        }
    }

    public ScreenMarkerTrackerHandle TrackWithMarkerWhileOffscreen(Transform target, bool isReleaseOnceOnScreen = true)
    {
        // Won't check for duplicates for now
        if (target == null)
        {
            return new ScreenMarkerTrackerHandle(ScreenMarkerTrackerHandle.INVALID_HANDLE);
        }

        if (_availableMarkers.Count < 0)
        {
            return new ScreenMarkerTrackerHandle(ScreenMarkerTrackerHandle.INVALID_HANDLE);
        }

        ScreenMarkerTracker tracker = _availableMarkers.Dequeue();
        tracker.Assigned(target, isReleaseOnceOnScreen);
        _activeMarkers.Add(tracker);
        return tracker.AssignedHandle;
    }

    public void ReleaseScreenTracker(ScreenMarkerTrackerHandle handle)
    {
        if (!IsTrackerHandleValid(handle))
        {
            return;
        }

        ScreenMarkerTracker tracker = _markers[handle.Handle];
        tracker.Target = null;
        tracker.Marker.gameObject.SetActive(false);

        _availableMarkers.Enqueue(tracker);
        _activeMarkers.Remove(tracker);
    }

    public bool IsTrackerHandleValid(ScreenMarkerTrackerHandle handle)
    {
        return handle.IsValid && handle.Handle < _poolSize;
    }
}

public struct ScreenMarkerTrackerHandle
{
    public ScreenMarkerTrackerHandle(int handle)
    {
        _handle = handle;
    }

    public const int INVALID_HANDLE = -1;

    private int _handle;
    public int Handle => _handle;

    public bool IsValid => Handle > INVALID_HANDLE;
}