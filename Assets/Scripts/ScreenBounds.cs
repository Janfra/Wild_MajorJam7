using System;
using System.Linq;
using FMODUnity;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public static class ScreenBounds
{
    static public bool IsObjectWithinCameraView(Vector3 position, Camera camera)
    {
        if (!camera) return false;

        Vector3 screenPosition = camera.WorldToScreenPoint(position);
        return IsScreenPointWithinScreen(screenPosition);
    }

    /// <summary>
    /// Returns a screen position on the edge closest to target offscreeen that has been offset by value
    /// </summary>
    /// <returns>Was the given position offscreen</returns>
    static public bool GetOffsetBorderPositionForOffscreenObject(Vector3 position, Camera camera, Vector3 offset, out Vector3 offsetScreenEdgePosition)
    {
        offsetScreenEdgePosition = Vector3.zero;
        if (camera == null)
        {
            return false;
        }

        Plane[] cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);
        if (cameraFrustum.All(cameraFrustum => cameraFrustum.GetDistanceToPoint(position) >= 0))
        {
            return false;
        }

        Vector3[] corners = new Vector3[4];
        camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, corners);
        Vector3 cornerA = camera.transform.TransformPoint(corners[0]);
        Vector3 cornerB = camera.transform.TransformPoint(corners[1]);
        Vector3 cornerC = camera.transform.TransformPoint(corners[2]);
        Vector3 cornerScreenA = camera.WorldToScreenPoint(cornerA);
        Vector3 cornerScreenB = camera.WorldToScreenPoint(cornerB);
        Vector3 cornerScreenC = camera.WorldToScreenPoint(cornerC);
        Vector3 screenPoint = camera.WorldToScreenPoint(position);

        Vector3 screenEdgePosition = Vector3.zero;
        screenEdgePosition.x = Mathf.Clamp(screenPoint.x, cornerScreenA.x, cornerScreenC.x);
        screenEdgePosition.y = Mathf.Clamp(screenPoint.y, cornerScreenA.y, cornerScreenB.y);

        // need to decide the offset direction based on dot products or similar, for now, leaving it
        screenEdgePosition += offset;
        offsetScreenEdgePosition = screenEdgePosition;
        return true;
    }

    static public bool IsScreenPointWithinScreen(Vector3 screenPoint)
    {
        return screenPoint.x <= 0.0f || screenPoint.x >= Screen.width || screenPoint.y <= 0.0f || screenPoint.y >= Screen.height;
    }
}
