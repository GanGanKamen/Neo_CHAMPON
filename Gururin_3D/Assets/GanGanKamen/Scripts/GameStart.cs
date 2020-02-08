using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    //public string NowScene { get { return nowSceneName; } }
    //public Platform Platform { get { return platform; } }

    public string NextSceneName;
    
    private string nowSceneName;
    private string preSceneName;
    static public Camera mainCamera;

    private float width = 1080f;
    private float height = 1920f;

    public string[] ignoreScenes;

    private Platform platform;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        nowSceneName = SceneManager.GetActiveScene().name;
        preSceneName = nowSceneName;

        SceneManager.LoadSceneAsync(NextSceneName);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ScreenSet();
        GetPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        GetSceneChange();
    }

    private void ScreenSet()
    {
        float currentRatio = Screen.width * 1f / Screen.height;
        float targetRatio = 16f / 9f;

        if (currentRatio < targetRatio)
        {
            float ratio = targetRatio / currentRatio - 1f;
            float rectY = ratio / 2f;
            mainCamera.rect = new Rect(0, rectY, 1f, 1f - ratio);
        }

        else if (currentRatio > targetRatio)
        {
            float ratio = targetRatio / currentRatio;
            float rectX = (1f - ratio) / 2f;
            mainCamera.rect = new Rect(rectX, 0, ratio, 1f);
        }
    }

    private void GetPlatform()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            platform = Platform.Android;
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            platform = Platform.iOS;
        }
        else
        {
            platform = Platform.Windows;
        }
    }
    private void GetSceneChange()
    {
        nowSceneName = SceneManager.GetActiveScene().name;
        if (preSceneName != nowSceneName)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var isIgnore = false;
            foreach (string name in ignoreScenes)
            {
                if (name == nowSceneName)
                {
                    isIgnore = true;
                }
            }
            if (isIgnore == false)
            {
                ScreenSet();
            }

            preSceneName = nowSceneName;
        }
    }
}
