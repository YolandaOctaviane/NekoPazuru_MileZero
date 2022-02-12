using UnityEngine;

namespace Belts
{
    public class ItemBehaviour : MonoBehaviour
    {
        public float Speed { get; set; } = 1;

        public ItemDirection Direction { get; set; }

        private bool Stop { get; set; }

        private Bounds _bounds;

        private void Start()
        {
            Direction = ItemDirection.Right;
        }

        public void MoveRight()
        {
            CheckItemCollision();

            if (Stop)
            {
                return;
            }
            
            var transformObject = transform;
            var position = transformObject.position;
            Vector3 nextPosition = new Vector3(position.x + Speed * Time.deltaTime, position.y, position.z);
            
            transformObject.position = nextPosition;
        }

        private void CheckItemCollision()
        {
            _bounds = GetComponent<Collider2D>().bounds;

            float offset = .3f;

            if (Direction == ItemDirection.Right)
            {
                var size = new Vector2(.01f, _bounds.size.y / 2);
                var point = new Vector2(_bounds.max.x + offset, _bounds.center.y);
                // ReSharper disable once Unity.PreferNonAllocApi
                var collider2Ds = Physics2D.OverlapBoxAll(point, size, 0);

                foreach (Collider2D colliderItem in collider2Ds)
                {
                    if (colliderItem.CompareTag(Tags.Item))
                    {
                        Stop = true;
                        return;
                    }
                }
            }

            Stop = false;
        }
    }
}
