using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] public bool isDed = false;
    [Header("Movement")]
    [SerializeField] Transform[] positions;
    [SerializeField] float speed;

    int posIndex;
    Transform nextPos;

    private void Start()
    {
        nextPos = positions[0];
    }

    private void Update()
    {
        catMove();
    }

    void catMove()
    {
        if (transform.position == nextPos.position)
        {
            posIndex++;
            if (posIndex >= positions.Length) return;
            nextPos = positions[posIndex];
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
        }
    }
}
