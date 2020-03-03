using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public string NextSceneName;

    [System.NonSerialized] public string nowSceneName;
    private string preSceneName;
    static public Camera mainCamera;

    private float width = 1080f;
    private float height = 1920f;

    [SerializeField] private RectTransform[] mask; //0 left,1 right,2 top,3 bottom
    public string[] ignoreScenes;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        nowSceneName = SceneManager.GetActiveScene().name;
        preSceneName = nowSceneName;

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ScreenSet();
        GetLanguage();
        SceneManager.LoadSceneAsync(NextSceneName);
        
    }

    // Update is called once per frame
    void Update()
    {
        GetSceneChange();
    }

    private void GetLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Japanese:
                LanguageSwitch.language = LanguageSwitch.Language.Japanese;
                break;
            case SystemLanguage.ChineseSimplified:
                LanguageSwitch.language = LanguageSwitch.Language.ChineseHans;
                break;
            case SystemLanguage.ChineseTraditional:
                LanguageSwitch.language = LanguageSwitch.Language.ChineseHant;
                break;
            default:
                LanguageSwitch.language = LanguageSwitch.Language.English;
                break;
        }
    }

    private void ScreenSet()
    {
        float currentRatio = Screen.width * 1f / Screen.height;
        float targetRatio = 16f / 9f;
        
        if(currentRatio < targetRatio)
        {
            float ratio = targetRatio / currentRatio - 1f;
            float rectY = ratio / 2f;
            mainCamera.rect = new Rect(0, rectY, 1f, 1f - ratio);
        }   

        else if(currentRatio > targetRatio)
        {
            float ratio = targetRatio / currentRatio;
            float rectX = (1f - ratio) / 2f;
            mainCamera.rect = new Rect(rectX, 0, ratio, 1f);
        }
    }

    private void GetSceneChange()
    {
        nowSceneName = SceneManager.GetActiveScene().name;
        if (preSceneName != nowSceneName)
        {
            NeoConfig.isSoundFade = false;
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var isIgnore = false;
            foreach(string name in ignoreScenes)
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
