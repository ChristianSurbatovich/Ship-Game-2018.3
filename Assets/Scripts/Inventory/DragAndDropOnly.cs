using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropOnly : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Vector3 mouseOffset;
    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseOffset = transform.position - Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + mouseOffset;
    }
}
