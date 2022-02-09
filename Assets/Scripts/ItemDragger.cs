using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private DragManager _dragManager;
    private ConveyorItem _item;
    private Camera _mainCamera;

    private void Awake()
    {
        _dragManager = FindObjectOfType<DragManager>();
        _item = GetComponent<ConveyorItem>();
        _mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragManager.isDragging = true;
        _dragManager.draggingItem = _item;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragManager.isDragging = true;
        _dragManager.draggingItem = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_dragManager.isDragging && _dragManager.draggingItem ==  _item)
        {
            var worldPos = _mainCamera.ScreenToWorldPoint(eventData.position);
            var position = new Vector3(worldPos.x, worldPos.y, 0);
            transform.position = position;
        }
    }
}
