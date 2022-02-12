using System;
using System.Collections;
using System.Collections.Generic;
using Conveyor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class BridgeDrop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private DragManager dragManager;
    [SerializeField] private GameObject bridge;
    [SerializeField] private ItemType type;
    //[SerializeField] private Bridge bridgeCollider;
    private AudioManager _audio;

    private void Awake()
    {
        dragManager = FindObjectOfType<DragManager>();
        _audio = FindObjectOfType<AudioManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");

        if (dragManager.draggingObject.GetComponent<ConveyorItem>().type == type)
        {
            _audio.Play("dropBridge");
            dragManager.DestroyObject();
            GetComponent<Bridge>().isBuilt = true;
            Destroy(dragManager.draggingObject);
            bridge.transform.DOScale(Vector3.one, .25f);
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dragManager.isDragging && dragManager.draggingObject != null && dragManager.draggingObject.tag == bridge.tag)
        {
            
            dragManager.isDragging = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!dragManager.isDragging && dragManager.draggingObject != null && dragManager.draggingObject.tag == bridge.tag)
        {
            dragManager.isDragging = true;
        }
    }
}
