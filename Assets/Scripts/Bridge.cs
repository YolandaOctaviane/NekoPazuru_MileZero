using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bridge : MonoBehaviour
{
    [SerializeField] private float timer = 2;
    [SerializeField] bool timeIsRunning = false;
    [SerializeField] public bool isBuilt = false;
    private float defaultTimer;
    private Cat cat;
    [SerializeField] TilemapRenderer tilemap;
    private int counter = 0;

    private void Awake()
    {
        defaultTimer = timer;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cat = collision.GetComponent<Cat>();
        if (cat != null)
        {
            timer = 100f;
            //death if no bridge
            if (!isBuilt)
            {
                cat.isDed = true;
            }
            counter++;
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cat = collision.GetComponent<Cat>();
        if (cat != null)
        {
            counter--;
        }
        if (counter == 0 && cat)
        {
            timer = 0f;
        }
    }

    private void Update()
    {
        if (isBuilt)
        {
            timeIsRunning = true;
            tilemap.enabled = true;
        }
        else
        {
            tilemap.enabled = false;
        }

        if (timeIsRunning)
        {
            if(timer > 0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timeIsRunning = false;
                timer = defaultTimer;
                isBuilt = false;
                tilemap.gameObject.transform.localScale = Vector3.zero;
            }
        }
    }

}
