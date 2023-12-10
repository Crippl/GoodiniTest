using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeHandler : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private CameraMovement cameraMovement;
    private Vector2 currentPosition;
    private Vector2 lastPosition;


    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPosition = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentPosition=eventData.position;
        cameraMovement.MoveBySwipe(currentPosition,lastPosition);
        lastPosition = currentPosition;
    }

}
