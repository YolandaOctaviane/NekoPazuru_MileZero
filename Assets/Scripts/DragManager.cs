using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    private bool _isDragging;
    private ConveyorItem _draggingItem;
        public bool isDragging
            { 
                get => _isDragging; 
                set => _isDragging = value; 
            }

        public ConveyorItem draggingItem
            { 
                get => _draggingItem; 
                set => _draggingItem = value; 
            }
        
    
}
