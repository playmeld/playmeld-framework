using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaneDetectionManager : MonoBehaviour
{
    private ARPlaneManager arPlaneManager;

    void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    public void EnablePlaneDetection()
    {
        SetPlaneDetection(true);
    }

    public void DisablePlaneDetection()
    {
        SetPlaneDetection(false);
    }

    public void SetPlaneDetection(bool value)
    {
        arPlaneManager.enabled = value;
        arPlaneManager.SetTrackablesActive(value);
    }
}