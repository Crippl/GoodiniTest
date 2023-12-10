using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Image joystickBackground;
    private Image movementJoystick;
    private Vector2 inputVector;
    private Vector2 startPosition;
    private bool joystickIsActive;

    private void Start()
    {
        joystickIsActive = false;
        movementJoystick=GetComponent<Image>();    
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickIsActive = true;
        startPosition = movementJoystick.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground.rectTransform,eventData.position,null, out joystickPosition))
        {
            joystickPosition.x = (joystickPosition.x / joystickBackground.rectTransform.sizeDelta.x);
            joystickPosition.y = (joystickPosition.y / joystickBackground.rectTransform.sizeDelta.y);
        }

        inputVector = new Vector2(joystickPosition.x,joystickPosition.y);
        if (inputVector.magnitude > 1f)
        {
            inputVector = inputVector.normalized;
        }

        movementJoystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBackground.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBackground.rectTransform.sizeDelta.y / 2));
        cameraMovement.MoveByJoystick(joystickIsActive,joystickPosition,startPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickIsActive = false;
        cameraMovement.MoveByJoystick(joystickIsActive, Vector2.zero, startPosition);
        inputVector = Vector2.zero;
        movementJoystick.rectTransform.anchoredPosition = Vector2.zero;
    }
}
