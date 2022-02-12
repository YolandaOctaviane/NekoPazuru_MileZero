using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Animator Overlay;
    [SerializeField] private Button buttonPause;
    [SerializeField] private AudioSource bgm;

    public static bool gameIsPaused;

    public void PauseBtn()
    {
        Overlay.SetTrigger("Pause");
        gameIsPaused = true;
        Time.timeScale = 0;
        bgm.volume = 0.4f;
    }

    public void Retry()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        Overlay.SetTrigger("Unpause");
        Time.timeScale = 1;
        gameIsPaused = false;
        bgm.volume = 1f;
    }
}


