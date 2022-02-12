using UnityEngine;

public class SceneIndexManager : MonoBehaviour
{
    private static SceneIndexManager _instance;

    public string indexScene;

    public string catColor;
    
    public static SceneIndexManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SceneIndexManager is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        indexScene = "Main";

        catColor = "empty";
        
        DontDestroyOnLoad(this);
    }
}
