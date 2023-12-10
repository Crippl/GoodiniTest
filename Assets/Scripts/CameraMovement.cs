using Cinemachine;
using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineOrbitalTransposer orbital;
    private CinemachineComposer composer;
    private readonly float magicNumberForYAxis = 1000f;
    private bool joystickIsActive;
    private Vector2 currentJoystickPosition;
    private Vector2 startJoystickPosition;

    private void Start()
    {
        joystickIsActive = false;
        CameraChanger.OnCameraChanged += CameraChanged;
    }

    private void Update()
    {
        if (joystickIsActive)
        {
            if (orbital != null)
            {
                orbital.m_XAxis.Value += Convert.ToInt32(currentJoystickPosition.x - startJoystickPosition.x);
                composer.m_ScreenY += (currentJoystickPosition.y - startJoystickPosition.y) / magicNumberForYAxis;
                if (composer.m_ScreenY > 0.9f) { composer.m_ScreenY = 0.9f; }
                if (composer.m_ScreenY < 0.1f) { composer.m_ScreenY = 0.1f; }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void MoveBySwipe(Vector2 currentPosition,Vector2 lastPosition)
    { 
        if (orbital != null)
        {
            orbital.m_XAxis.Value += Convert.ToInt32(currentPosition.x - lastPosition.x);
            composer.m_ScreenY += (currentPosition.y - lastPosition.y) / magicNumberForYAxis;
            if (composer.m_ScreenY > 0.9f) { composer.m_ScreenY = 0.9f; }
            if (composer.m_ScreenY < 0.1f) { composer.m_ScreenY = 0.1f; }
        }
    }

    public void MoveByJoystick(bool JoystickIsActive,Vector2 CurrentPosition,Vector2 StartPosition)
    {
        joystickIsActive=JoystickIsActive;
        currentJoystickPosition = CurrentPosition;
        startJoystickPosition = StartPosition;
    }

    private void CameraChanged(GameObject camera)
    {
        virtualCamera = camera.GetComponent<CinemachineVirtualCamera>();
        orbital = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        composer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
    }

    private void OnDestroy()
    {
        CameraChanger.OnCameraChanged -= CameraChanged;
    }
}
