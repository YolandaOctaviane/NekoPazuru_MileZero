using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSpot : MonoBehaviour, IDropHandler
{
    [SerializeField] private DragManager _dragManager;
    [SerializeField] private ConveyorType type;
    [SerializeField] private GameObject itemRenderer;

    private void Awake()
    {
        _dragManager = FindObjectOfType<DragManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var dragItem = _dragManager.draggingItem;
        if(dragItem.type == type)
        {
            Debug.Log("Sesuai");
            itemRenderer.SetActive(true);
        }
    }
}
