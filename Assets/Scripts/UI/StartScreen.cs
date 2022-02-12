using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Animator CanvasAnimator;
    [SerializeField] private GameObject CanvasAnimate;
    [SerializeField] private AudioSource SfxTransition;
    public bool startState = true;
    public bool stageState = true;

    public static bool StageDirect;

    private void Start()
    {
        if (StageDirect)
        {
            DirectOverlay();
            StageDirect = false;
        }
    }

    private void Update()
    {
        if (startState)
        {
            if (Input.GetButton("Fire1"))
            {
                CanvasAnimator.SetTrigger("Start");
                SfxTransition.Play();
                startState = false;
                StartCoroutine(PauseAnimator());
            } 
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackStart();
            }
        }
    }

    IEnumerator PauseAnimator()
    {
        yield return new WaitForSeconds(1.5f);
        CanvasAnimate.SetActive(false);
    }

    public void BackStart()
    {
        CanvasAnimate.SetActive(true);
        CanvasAnimator.SetTrigger("Back");
        startState = true;
    }


    public void OpenOverlay()
    {
        CanvasAnimator.SetTrigger("OpenOverlay");
        startState = false;
        stageState = true;
    }
    public void CloseOverlay()
    {
        CanvasAnimator.SetTrigger("CloseOverlay");
        startState = false;
        stageState = false;
    }

    public void DirectOverlay()
    {
        CanvasAnimator.SetTrigger("DirectOverlay");
        startState = false;
        stageState = true;
        CanvasAnimate.SetActive(false);
    }


}
