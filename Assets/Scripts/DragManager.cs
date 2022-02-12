using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Belts;
using UnityEngine;
using UnityEngine.XR;

public class DragManager : MonoBehaviour
{
    public bool isDragging = false;
    public GameObject draggingObject;

    public GameObject fishPrefab;


    public void DestroyObject()
    {
        draggingObject.GetComponent<ItemDestroyer>().DestroyThis();
    }
}
