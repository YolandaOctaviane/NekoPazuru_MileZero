using System.Collections;
using UnityEngine;

public class DeathEvent : MonoBehaviour
{
    public Animator canvasMenu;
    
    void Start()
    {
        StartCoroutine(PauseAnimator());
    }

    IEnumerator PauseAnimator()
    {
        yield return new WaitForSeconds(2f);
        canvasMenu.SetTrigger("Pause");
    }
}
