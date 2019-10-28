using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    //public Data data;
    //public GameObject configCanvas;

    public SceneChange sceneChange;

    //効果音
    private CriAtomSource _startSE;
    private bool _fadeOut;

    private int _cnt;
    private float _volume;

    private Configuration config;

    public bool isBackToTitle;
    // Start is called before the first frame update
    void Start()
    {
        //if (GameObject.Find("Data") != null) data = GameObject.Find("Data").GetComponent<Data>();
        //configCanvas = GameObject.Find("ConfigCanvas");
        _startSE = GetComponent<CriAtomSource>();

        _volume = 1.0f;
        _fadeOut = false;

        config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeOut)
        {
            _volume -= 0.01f;
            //BGMのボリュームを段階的に下げる
            CriAtom.SetCategoryVolume("BGM", _volume);

            if (_volume == 0.0f) _volume = 0.0f;
        }
    }
    public void OnClick()
    {

        _cnt++;
        //一度だけStartSEを鳴らす
        if (_cnt == 1)
        {
            _startSE.Play();
            _cnt = 0;
        }
        //_fadeOut = true;
        //Debug.Log("CLICK");
        if(sceneChange.button == false)sceneChange.button = true;
        if (isBackToTitle) sceneChange.isBackToTitle = true;
        RemainingLife.bossLife = 0;
        config.configbutton = false;
        /*if(SceneManager.GetActiveScene().name == "Title")
        {

        }
        else
        {
            //Destroy(configCanvas);
        }*/
    }

    public void TitleOnclick(string name)
    {
        SoundManager.PlayS(gameObject);
        Fader.FadeIn(2f, name);
    }
}
