using UnityEngine;

namespace Conveyor
{
    public class ConveyorItemMover : MonoBehaviour
    {
        public DictionaryItemsGenerator generator;
        public Transform target;
        public int position;
        public bool canMove;

        public int startPosition;
        private float _timer;

        private void Update()
        {
            if(canMove)
                Move();
        
        }

        public void Init(DictionaryItemsGenerator generator, int position, bool canMove, int startPosition)
        {
            this.generator = generator;
            this.position = position;
            this.canMove = canMove;
            this.startPosition = startPosition;
            target = this.generator.gridBelts[this.position].transform;
        }

        private void Move()
        {
            _timer += Time.deltaTime / 3;
            transform.position = Vector2.Lerp(generator.gridBelts[startPosition].transform.position, 
                generator.gridBelts[position].transform.position, 
                _timer);
        }
    }
}
