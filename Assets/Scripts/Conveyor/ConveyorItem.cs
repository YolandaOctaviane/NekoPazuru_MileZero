using UnityEngine;

namespace Conveyor
{
    public class ConveyorItem : MonoBehaviour
    {
        public ItemType type;
    }

    public enum ItemType
    {
        Fish, VerticalBridge, HorizontalBridge
    }
}

