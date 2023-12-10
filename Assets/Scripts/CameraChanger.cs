using System;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private GameObject mainVirtualCamera;
    private GameObject currentCamera;

    public static Action<GameObject> OnCameraChanged;

    private void Awake()
    {
        currentCamera = mainVirtualCamera;
    }

    public void ChangeFocus(GameObject connectedCamera)
    {
        if (currentCamera != null)
        {
            currentCamera.SetActive(false);
        }
        currentCamera = connectedCamera;
        currentCamera.SetActive(true);

        if (OnCameraChanged != null)
        {
            OnCameraChanged?.Invoke(currentCamera);
        }
    }
}

