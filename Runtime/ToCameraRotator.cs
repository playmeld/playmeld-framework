using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ToCameraRotator : MonoBehaviour
{
    private Transform MainCamera;

    public void Awake()
    {
        MainCamera = ComponentLocatorUtility<ARCameraManager>.FindComponent().transform;
    }

    public void RotateToCamera(GameObject gameObject)
    {
        gameObject.transform.rotation = Quaternion.Euler(
            gameObject.transform.eulerAngles.x,
            Camera.main.transform.eulerAngles.y + 180.0f,
            gameObject.transform.eulerAngles.z
        );
    }
}
