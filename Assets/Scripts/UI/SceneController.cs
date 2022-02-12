using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Animator transition;
        [SerializeField] private float transitionTime = 1f;

        public void PindahScene(string scene)
        {
            StartCoroutine(LoadScene(scene));

            SceneIndexManager.Instance.indexScene = scene;
        }
    
        IEnumerator LoadScene(string m_scene)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(m_scene);
        }


    }
}
