using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Configuration : MonoBehaviour
{
    public static Configuration instance;

    public GameObject configbuttonOpen;
    public GameObject configbuttonClose;
    public GameObject configwindow;
    public GameObject titleback;

    public float sensitivity, flickdistance;
    public int controllerposition;
    public bool configbutton = false;

    private CriAtomSource _open, _close;

    [SerializeField] FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (configbuttonClose == null)
        {
            configbuttonClose = GameObject.Find("ConfigButton_Close");
        }
        if(configbuttonOpen == null)
        {
            configbuttonOpen = GameObject.Find("ConfigButton_Open");
        }
        if(configwindow == null)
        {
            configwindow = GameObject.Find("ConfigWindow");
        }
        

        //SE追加
        _open = GameObject.Find("SE_WindowOpen(CriAtomSource)").GetComponent<CriAtomSource>();
        _close = GameObject.Find("SE_WindowClose(CriAtomSource)").GetComponent<CriAtomSource>();

        // イベントにイベントハンドラーを追加
        //SceneManager.sceneLoaded += SceneLoaded;
        

        configbuttonClose.SetActive(true);
        configbuttonOpen.SetActive(false);
        configwindow.SetActive(false);

        sensitivity = 1.5f;
        flickdistance = 0.01f;
        controllerposition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (flagManager == null)
        {
            flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        }
    }

    public void Method()
    {
        if (configbuttonOpen.activeSelf ==false)
        {
            _open.Play();
            //configbuttonClose.SetActive(false);
            configbuttonOpen.SetActive(true);
            configwindow.SetActive(true);
            configbutton = true;

            if(titleback != null)titleback.SetActive(true);

            
        }
        else
        {
            _close.Play();
            //configbuttonClose.SetActive(true);
            configbuttonOpen.SetActive(false);
            configwindow.SetActive(false);
            configbutton = false;

            if (titleback != null) titleback.SetActive(false);
        }
    }

    //第一引数(遷移後のシーン),第二引数(シーンの読み込みモード(Single or Additive))
    /*void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {

        if (nextScene.name == "Title")
        {
            _close.Play();
            configbuttonClose.SetActive(true);
            configbuttonOpen.SetActive(false);
            configwindow.SetActive(false);
            configbutton = false;
        }
        else if(nextScene.name == "Result")
        {

        }
        else
        {
            flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        }
    }*/
}
