using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fish : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    DragManager dragManager;
    [SerializeField] public bool isPut = false;
    private AudioManager _audio;
    private void Awake()
    {
        dragManager = FindObjectOfType<DragManager>();
        _audio = FindObjectOfType<AudioManager>();

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var cat = collision.GetComponent<Cat>();
        if (cat != null)
        {
            _audio.Play("eat");
            cat.isEating = true;
            Destroy(gameObject);
        }
    }
}
