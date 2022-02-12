using Conveyor;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Belts
{
    public class ItemDragger : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] DragManager dragManager;
        [SerializeField] bool isPut;
        [SerializeField] private ConveyorItemMover mover;

        private void Awake()
        {
            mainCamera = Camera.main;
            dragManager = FindObjectOfType<DragManager>();
            mover = GetComponent<ConveyorItemMover>();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            mover.canMove = false;
            
            var o = gameObject;
            
            o.layer = 2;
            dragManager.isDragging = true;
            dragManager.draggingObject = o;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dragManager.isDragging && dragManager.draggingObject != null)
            {
                var worldPos = mainCamera.ScreenToWorldPoint((eventData.position));
                var position = new Vector3(worldPos.x, worldPos.y, 0);
                transform.position = position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            gameObject.layer = 0;
            dragManager.isDragging = false;
            dragManager.draggingObject = null;
            
            mover.transform.DOMove(mover.target.position, .5f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var cat = collision.GetComponent<Cat>();
            if (cat != null & isPut)
            {
                cat.isEating = true;
                Destroy(gameObject);
            }
        }
    }
}
