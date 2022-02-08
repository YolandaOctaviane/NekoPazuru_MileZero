using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bridge : MonoBehaviour
{
    [SerializeField] private float timer = 2;
    [SerializeField] bool timeIsRunning = false;
    private float defaultTimer;
    private Cat cat;

    private void Start()
    {
        defaultTimer = timer;

        if (GetComponent<SpriteRenderer>().enabled)
        {
            timeIsRunning = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cat = collision.GetComponent<Cat>();
        if (cat != null)
        {
            timeIsRunning = false;
            //death if no bridge
            if (GetComponent<SpriteRenderer>().enabled == false)
            {
                cat.isDed = true;
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cat = collision.GetComponent<Cat>();
        if (cat != null)
        {
            timeIsRunning = false;
            timer = defaultTimer;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        if (timeIsRunning)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timeIsRunning = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
