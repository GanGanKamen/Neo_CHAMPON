using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public string NextSceneName;

    [System.NonSerialized] public string nowSceneName;
    private string preSceneName;
    static public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        nowSceneName = SceneManager.GetActiveScene().name;
        preSceneName = nowSceneName;

        SceneManager.LoadSceneAsync(NextSceneName);
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        GetSceneChange();
    }

    private void GetSceneChange()
    {
        nowSceneName = SceneManager.GetActiveScene().name;
        if (preSceneName != nowSceneName)
        {
            Debug.Log("SceneChange");
            NeoConfig.isSoundFade = false;
            mainCamera = Camera.main;
            preSceneName = nowSceneName;
        }
    }
}
