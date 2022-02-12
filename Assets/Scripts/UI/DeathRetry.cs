using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class DeathRetry : MonoBehaviour
    {
        public List<GameObject> catColorObject;

        private GameObject _indexManager;

        private AudioManager audioManager;


        private void Start()
        {
            _indexManager = GameObject.Find("SceneIndexManager");
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play("drown");

            switch (_indexManager.GetComponent<SceneIndexManager>().catColor)
            {
                case "belang":
                    catColorObject[0].SetActive(true);
                    break;
                case "gray":
                    catColorObject[1].SetActive(true);
                    break;
                case "orange":
                    catColorObject[2].SetActive(true);
                    break;
                default:
                    Debug.LogWarning("Cat color out of range!");
                    break;
            }
        }

        public void GotoMain()
        {
            SceneManager.LoadScene("Main");
        }

        public void GotoPreviousScene()
        {
            var previousScene = _indexManager.GetComponent<SceneIndexManager>().indexScene;
            SceneManager.LoadScene(previousScene);
        }
    }
}
