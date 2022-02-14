using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void doquit()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}
