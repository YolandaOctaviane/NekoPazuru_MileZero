using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Conveyor
{
    public class ItemsGenerator : MonoBehaviour
    {
        private GameObject _spawnPosition;
        
        [Header("Loot Table")]
        public int[] table = { 60, 40 };
        public int total;
        public int randomNumber;

        [Header("Items")] 
        public List<GameObject> bridge;
        public GameObject fish;
        private int _bridgeVerticalCount;
        private int _bridgeHorizontalCount;

        [Header("Timer")] 
        public float timer;
        private float _countDown;
        private bool _isTimer;
        
        // Start is called before the first frame update
        void Start()
        {
            _spawnPosition = GameObject.FindGameObjectWithTag(Belts.Tags.BeltSpawnPos);
            _countDown = timer;
            _isTimer = true;
            
            // init Total Items in Table
            foreach (var item in table)
            {
                total += item;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_isTimer)
            {
                TimerCountDown();
            }
        }

        private void LootTable()
        {
            /*
             * <summery>
             * 1. tally the total weight
             * 2. draw a random number between 0 and the total weight.
             *
             * ex. randomNumber = 49
             * is 49 <= 60 = table[0]
             *
             * ex. randomNumber = 61
             * is 61 <= 60 ?? -> no
             * 61 - 60 = 1
             * is 1 <= 40 = table[1]
             *
             * Q: what if more than 2 values in the table?
             * A: Suyatna said "maybe can do, I dunno guys  ¯\_(ツ)_/¯"
             * </summery>
             */

            randomNumber = Random.Range(0, total);

            foreach (var weight in table)
            {
                // compare is my random number <= current weight ?
                if (randomNumber <= weight)
                {
                    // execute table
                    GenerateItem(weight);
                    return;
                }

                randomNumber -= weight;
            }
        }

        private void GenerateItem(int weight)
        {
            switch (weight)
            {
                case 80:
                    BridgeGenerator();
                    break;
                case 20:
                    FishMaker();
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

                if (item > 5 && _bridgeVerticalCount < 2)
                {
                    BridgeVerticalMaker();
                }
                else if (item <= 5 && _bridgeHorizontalCount < 2)
                {
                    BridgeHorizontalMaker();
                }
                else
                { 
                    continue;
                }

                break;
            }
        }

        private void BridgeVerticalMaker()
        {
            _bridgeVerticalCount += 1;
            _bridgeHorizontalCount = 0;
                
            Instantiate(bridge[0], _spawnPosition.transform.position, Quaternion.identity);
        }
        
        private void BridgeHorizontalMaker()
        {
            _bridgeVerticalCount = 0;
            _bridgeHorizontalCount += 1;

            Instantiate(bridge[1], _spawnPosition.transform.position, Quaternion.identity);
        }

        private void FishMaker()
        {
            _bridgeHorizontalCount = 0;
            _bridgeVerticalCount = 0;
            
            Instantiate(fish, _spawnPosition.transform.position, Quaternion.identity);
        }
        
        private void TimerCountDown()
        {
            _countDown -= Time.deltaTime;

            if (!(_countDown < 0)) return;
            
            LootTable();
            
            _countDown = timer;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Item"))
            {
                _isTimer = false;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Item"))
            {
                _isTimer = true;
            }
        }
    }
}
