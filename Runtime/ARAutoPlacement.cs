using System;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;

[Serializable]
public class ARAutoPlacementEvent : UnityEvent<GameObject>
{
}

public class ARAutoPlacement : MonoBehaviour
{
    [SerializeField] private GameObject placementPrefab;
    [SerializeField] private XROrigin xrOrigin;
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private ARPlaneDetectionManager arPlaneDetectionManager;
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private float minDistanceFromOrigin = 0.5f;
    [SerializeField]
    ARAutoPlacementEvent onObjectPlaced = new ARAutoPlacementEvent();

    void Awake()
    {
        arPlaneManager.planesChanged += OnPlanesChanged;
    }

    void onDestroy()
    {
        arPlaneManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (TryGetPlacementPose(out Pose pose))
        {
            var placementObject = PlaceObject(pose);
            arPlaneManager.planesChanged -= OnPlanesChanged;
            arPlaneDetectionManager.DisablePlaneDetection();
            onObjectPlaced.Invoke(placementObject);
        }
    }

    private bool TryGetPlacementPose(out Pose pose)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Camera camera = xrOrigin.Camera;
        Debug.Log($"camera.transform.position: {camera.transform.position}");
        bool didHit = GestureTransformationUtility.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, xrOrigin);
        if (didHit)
        {
            foreach (var hit in hits)
            {
                pose = hit.pose;
                Vector2 poseVector2 = new Vector2(pose.position.x, pose.position.z);
                Vector2 cameraVector2 = new Vector2(camera.transform.position.x, camera.transform.position.z);
                float distance = Vector2.Distance(poseVector2, cameraVector2);
                if (distance < minDistanceFromOrigin)
                {
                    return true;
                }
            }
        }
        pose = default;
        return false;
    }

    private GameObject PlaceObject(Pose pose)
    {
        var placementObject = Instantiate(placementPrefab, pose.position, pose.rotation);

        // Create anchor to track reference point and set it as the parent of placementObject.
        var anchor = new GameObject("PlacementAnchor").transform;
        anchor.position = pose.position;
        anchor.rotation = pose.rotation;
        placementObject.transform.parent = anchor;

        // Use Trackables object in scene to use as parent
        var trackablesParent = xrOrigin.TrackablesParent;
        if (trackablesParent != null)
            anchor.parent = trackablesParent;

        return placementObject;
    }
}