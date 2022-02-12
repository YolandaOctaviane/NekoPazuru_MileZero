using System.Collections;
using System.Collections.Generic;
using Conveyor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class IkanDropSpot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] DragManager dragManager;
    [SerializeField] private GameObject fishPrefab;
    public Fish fish;

    private void Awake()
    {
        dragManager = FindObjectOfType<DragManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (dragManager.isDragging && dragManager.draggingObject != null &&
            dragManager.draggingObject.GetComponent<ConveyorItem>().type == ItemType.Fish)
        {
            dragManager.DestroyObject();
            Destroy(dragManager.draggingObject);
            Instantiate(dragManager.fishPrefab, transform.position, Quaternion.identity).transform.DOScale(Vector3.one, .25f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        // if (dragManager.isDragging && dragManager.draggingObject != null && 
        //     dragManager.draggingObject.GetComponent<ConveyorItem>().type == ItemType.Fish)
        // {
        //     dragManager.isDragging = false;
        //     dragManager.draggingObject.transform.position = transform.position;
        // }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // if(!dragManager.isDragging && dragManager.draggingObject != null  && 
        //    dragManager.draggingObject.GetComponent<ConveyorItem>().type == ItemType.Fish)
        //     dragManager.isDragging = true;
    }
}
