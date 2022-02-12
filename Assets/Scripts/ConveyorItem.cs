using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    public ConveyorType type;
}

public enum ConveyorType
{
    Fish, BridgeHorizontal, BridgeVertical  
}
