using UnityEngine;

namespace Belts
{
    public class RightBeltBehaviour : BeltBehaviour
    {
        protected override void DetectItems()
        {
            Transform transformObject;
            Bounds bounds = (transformObject = this.transform).GetComponent<Collider2D>().bounds;
            Vector2 size = bounds.size;

            // ReSharper disable once Unity.PreferNonAllocApi
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transformObject.position, size, 0);
            foreach (Collider2D colliderItem in colliders)
            {
                if (colliderItem.CompareTag(Tags.Item))
                {
                    Transform item = colliderItem.GetComponent<Transform>();
                    Bounds itemBounds = item.GetComponent<Collider2D>().bounds;
                    Vector2 itemPoint = new Vector2(itemBounds.min.x, itemBounds.max.y);
                    
                    // Always move when the item is exactly or more on top of the collider
                    if (!bounds.Contains(itemPoint))
                    {
                        continue;
                    }

                    ItemBehaviour itemBehaviour = item.GetComponent<ItemBehaviour>();
                    itemBehaviour.Direction = ItemDirection.Right;
                    itemBehaviour.Speed = 2f;
                    itemBehaviour.MoveRight();
                }
            }
        }
    }
}
