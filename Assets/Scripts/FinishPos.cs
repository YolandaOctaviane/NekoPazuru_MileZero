using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPos : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private int catCountToWin;
    [SerializeField] private Animator VictoryCanvas;
    private int _catCount;
    private Cat cat;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cat = collision.GetComponent<Cat>();
        if (cat != null)
        {
            _catCount++;
            PlaySound();
        }
    }

    private void Update()
    {
        if(_catCount == catCountToWin)
        {
            // win
            //Invoke("NextScene",3);
            VictoryCanvas.SetTrigger("Victory");


        }
    }

    private void NextScene()
    {
        SceneIndexManager.Instance.indexScene = nextScene;
        SceneManager.LoadScene(nextScene);
    }

    private void PlaySound()
    {
        if (cat.isRed)
        {
            audioManager.Play("cat1");
            Debug.Log("Play red cat sound");
        }
        if (cat.isBlue)
        {
            audioManager.Play("cat2");
        }
        if (cat.isYellow)
        {
            audioManager.Play("cat3");
        }
    }
}
