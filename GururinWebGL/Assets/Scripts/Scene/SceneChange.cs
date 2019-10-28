using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移制御
/// 主にステージクリア時
/// </summary>

public class SceneChange : MonoBehaviour
{

    public SceneObject changeScene;
    public bool button;

    private FlagManager flagManager;

    private bool _volumeDown;
    private float _volumeBGM,_volumeSE;
    private float bgmDecay, seDecay;
    public SceneObject[] bossScenes;

    private Configuration config;

    public bool isBackToTitle;
    // Start is called before the first frame update
    void Start()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();

        button = false;
        _volumeDown = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            flagManager.velXFixed = true;
            //ぐるりんの動きを止める
            flagManager.moveStop = true;
            //3.5秒遅延を入れる(ゴールSEを入れるため)
            if (_volumeDown == false)
            {
                Invoke("Scene", 3.5f);
                NeoConfig.isSoundFade = true;
                _volumeBGM = NeoConfig.BGMVolume/10f;
                _volumeSE = NeoConfig.SEVolume/10f;
                bgmDecay = (NeoConfig.BGMVolume / 10 * Time.deltaTime) / 3.5f;
                seDecay = (NeoConfig.SEVolume / 10 * Time.deltaTime) / 3.5f;
                _volumeDown = true;
            }
        }
    }

    void Scene()
    {
        NeoConfig.isSoundFade = false;
        bool isBoss = false;
        foreach (SceneObject scene in bossScenes)
        {
            if (scene.ToString() == changeScene.ToString())
            {
                isBoss = true;
            }
        }
        if (isBoss) RemainingLife.beforeBossLife = RemainingLife.life;
        SceneManager.LoadScene(changeScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (_volumeDown)
        {
            //ゴール時にボリュームをマイナスする(フェードアウト)
            //_volume -= 0.01f;
            //BGMカテゴリとSEカテゴリを指定
            _volumeBGM -= bgmDecay;
            if (_volumeBGM < 0) _volumeBGM = 0;
            _volumeSE -= seDecay;
            if (_volumeSE < 0) _volumeSE = 0;
            CriAtom.SetCategoryVolume("BGM", _volumeBGM);
            CriAtom.SetCategoryVolume("SE", _volumeSE);
            Debug.Log(_volumeBGM);
        }
        if (button)
        {
            NeoConfig.isSoundFade = false;
            if (_volumeDown == false)
            {
                NeoConfig.isSoundFade = true;
                _volumeBGM = NeoConfig.BGMVolume/10f;
                if (_volumeBGM < 0) _volumeBGM = 0;
                _volumeSE = NeoConfig.SEVolume/10f;
                if (_volumeSE < 0) _volumeSE = 0;
                bgmDecay = (NeoConfig.BGMVolume / 10 * Time.deltaTime) / 1f;
                seDecay = (NeoConfig.SEVolume / 10 * Time.deltaTime) / 1f;
                _volumeDown = true;
                Invoke("GameStart", 1.5f);
            }

        }
    }

    void GameStart()
    {
        _volumeDown = false;
        button = false;
        NeoConfig.isSoundFade = false;
        bool isBoss = false;
        foreach (SceneObject scene in bossScenes)
        {
            if (scene.ToString() == changeScene.ToString())
            {
                isBoss = true;
            }
        }
        if (isBoss) RemainingLife.beforeBossLife = RemainingLife.life;
        if (isBackToTitle == true) config.Method();
        SceneManager.LoadScene(changeScene);
    }
}
