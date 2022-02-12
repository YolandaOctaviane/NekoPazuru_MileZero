using System.Collections.Generic;
using Belts;
using UnityEngine;

namespace Conveyor
{
    public class DictionaryItemsGenerator : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject fish;
        [SerializeField] private GameObject verticalBridge;
        [SerializeField] private GameObject horizontalBridge;
        
        [Header("Belts")]
        public List<GameObject> gridBelts;
        public List<ConveyorItem> valueBelts;
        public List<GameObject> itemsPosition;

        [Header("Canvas")]
        public GameObject canvasBelt;
        
        [Header("Table Item")]
        public int[] tableItem = { 80, 20 };
        public int totalItems;

        public int position;
        
        private float _timer = 3f;

        private int _verticalBridgeCount;
        private int _horizontalBridgeCount;

        // Start is called before the first frame update
        void Start()
        {
            canvasBelt = GameObject.Find("CanvasBelt");

            // init Total Items in Table
            foreach (var item in tableItem)
            {
                totalItems += item;
            }

            CheckValueBelts();
            
            ItemStarter();
        }

        // Update is called once per frame
        void Update()
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0 && position > 1)
            {
                GenerateItem(Random.Range(0, totalItems));
            
                position--;

                _timer = 3f;
            }
        }

        public void ShiftItemsInList(int pos)
        {
            for (int i = pos; i > position; i--)
            {
                valueBelts[i] = valueBelts[i - 1];
                itemsPosition[i] = itemsPosition[i - 1];

                if (i - 1 == position)
                {
                    valueBelts[i - 1] = null;
                }
                else
                {
                    GameObject go = itemsPosition[i - 1];
                    
                    var mover = go.GetComponent<ConveyorItemMover>();
                    mover.Init(this, i, true, 0);
                    
                    var destroyer = go.GetComponent<ItemDestroyer>();
                    destroyer.CheckPosition(this, i);
                }
            }

            position++;
        }
        
        /// <summary>
        ///  1-19 : Fish
        /// 20 - 59 : Horizontal
        /// 60 - 100 : Vertical 
        /// </summary>
        /// <param name="seed"></param>
        private void GenerateItem(int seed)
        {
            foreach (var weight in tableItem)
            {
                // compare is my random number <= current weight ?
                if (seed <= weight)
                {
                    // execute table
                    ChooseItem(weight);
                    return;
                }

                seed -= weight;
            }
        }

        private void CheckValueBelts()
        {
            foreach (var value in valueBelts)
            {
                if (value == null)
                {
                    position++;
                }
            }

            position--;
        }
        
        private void ChooseItem(int weight)
        {
            switch (weight)
            {
                case 80:
                    BridgeGenerator();
                    break;
                case 20:
                    FishMaker(position);
                    break;
                default:
                    Debug.Log("Invalid weight");
                    break;
            }
        }

        private void BridgeGenerator()
        {
            while (true)
            {
                int item = Random.Range(0, 10);

                if (item > 5 && _verticalBridgeCount < 2)
                {
                    BridgeVerticalMaker(position);
                }
                else if (item <= 5 && _horizontalBridgeCount < 2)
                {
                    BridgeHorizontalMaker(position);
                }
                else
                { 
                    continue;
                }

                break;
            }
        }

        private void FishMaker(int positions)
        {
            _horizontalBridgeCount = 0;
            _verticalBridgeCount = 0;
            
            GameObject go = Instantiate(fish,
                new Vector3(gridBelts[0].transform.position.x, gridBelts[0].transform.position.y, 0),
                Quaternion.identity);
            var mover = go.GetComponent<ConveyorItemMover>();
            var item = go.GetComponent<ConveyorItem>();
            mover.Init(this, positions, true, 0);
            
            valueBelts[positions] = item;
            itemsPosition[positions] = go;
            
            var destroyer = go.GetComponent<ItemDestroyer>();
            destroyer.CheckPosition(this, positions);
            
            if (go != null)
            {
                go.transform.SetParent(canvasBelt.transform);
            }
        }

        private void BridgeVerticalMaker(int positions)
        {
            _verticalBridgeCount += 1;
            _horizontalBridgeCount = 0;
            
            GameObject go = Instantiate(verticalBridge, 
                new Vector3(gridBelts[0].transform.position.x, gridBelts[0].transform.position.y, 0), 
                Quaternion.identity);
            var mover = go.GetComponent<ConveyorItemMover>();
            var item = go.GetComponent<ConveyorItem>();
            mover.Init(this, positions, true, 0);
            
            valueBelts[positions] = item;
            itemsPosition[positions] = go;

            var destroyer = go.GetComponent<ItemDestroyer>();
            destroyer.CheckPosition(this, positions);
            
            if (go != null)
            {
                go.transform.SetParent(canvasBelt.transform);
            }
        }

        private void BridgeHorizontalMaker(int positions)
        {
            _verticalBridgeCount = 0;
            _horizontalBridgeCount += 1;
            
            GameObject go = Instantiate(horizontalBridge,
                new Vector3(gridBelts[0].transform.position.x, gridBelts[0].transform.position.y, 0),
                Quaternion.identity);
            var mover = go.GetComponent<ConveyorItemMover>();
            var item = go.GetComponent<ConveyorItem>();
            mover.Init(this, positions, true, 0);
            
            valueBelts[positions] = item;
            itemsPosition[positions] = go;

            var destroyer = go.GetComponent<ItemDestroyer>();
            destroyer.CheckPosition(this, positions);
            
            if (go != null)
            {
                go.transform.SetParent(canvasBelt.transform);
            }
        }

        private void ItemStarter()
        {
            GameObject horizontalBridgeObject = Instantiate(horizontalBridge,
                new Vector3(gridBelts[0].transform.position.x, gridBelts[0].transform.position.y, 0),
                Quaternion.identity);
            var horizontalBridgeMover = horizontalBridgeObject.GetComponent<ConveyorItemMover>();
            var horizontalBridgeItem = horizontalBridgeObject.GetComponent<ConveyorItem>();
            horizontalBridgeMover.Init(this, 11, true, 11);

            valueBelts[11] = horizontalBridgeItem;
            itemsPosition[11] = horizontalBridgeObject;
            
            var horizontalDestroyer = horizontalBridgeObject.GetComponent<ItemDestroyer>();
            horizontalDestroyer.CheckPosition(this, 11);
            
            if (horizontalBridgeObject != null)
            {
                horizontalBridgeObject.transform.SetParent(canvasBelt.transform);
            }
            
            GameObject verticalBridgeObject = Instantiate(verticalBridge, 
                new Vector3(gridBelts[0].transform.position.x, gridBelts[0].transform.position.y, 0), 
                Quaternion.identity);
            var verticalBridgeMover = verticalBridgeObject.GetComponent<ConveyorItemMover>();
            var verticalBridgeItem = verticalBridgeObject.GetComponent<ConveyorItem>();
            verticalBridgeMover.Init(this, 10, true, 10);
            
            valueBelts[10] = verticalBridgeItem;
            itemsPosition[10] = verticalBridgeObject;

            var verticalDestroyer = verticalBridgeObject.GetComponent<ItemDestroyer>();
            verticalDestroyer.CheckPosition(this, 10);

            if (verticalBridgeObject != null)
            {
                verticalBridgeObject.transform.SetParent(canvasBelt.transform);
            }
            
            GameObject fishObject = Instantiate(fish, 
                new Vector3(gridBelts[0].transform.position.x, gridBelts[0].transform.position.y, 0), 
                Quaternion.identity);
            var fishMover = fishObject.GetComponent<ConveyorItemMover>();
            var fishItem = fishObject.GetComponent<ConveyorItem>();
            fishMover.Init(this, 9, true, 9);
            
            valueBelts[9] = fishItem;
            itemsPosition[9] = fishObject;

            var fishDestroyer = fishObject.GetComponent<ItemDestroyer>();
            fishDestroyer.CheckPosition(this, 9);

            if (fishObject != null)
            {
                fishObject.transform.SetParent(canvasBelt.transform);
            }
            
            position -= 3;
        }
    }
}
