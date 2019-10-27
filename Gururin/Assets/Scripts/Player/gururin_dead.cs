using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;

public class gururin_dead : MonoBehaviour
{

    public SceneObject reloadScene;
    public GameObject vCam;

    public Data data;

    private CinemachineBrain _cinemachineBrain;
    private float _cameraBlend;
    private CanvasGroup _gameOver;
    private FlagManager _flagManager;
    private CriAtomSource _gameOverSE;
    public bool _SEPlay;
    private Text _lifeCount;
    [SerializeField] private GanGanKamen.DeadZone deadZone;
    // Start is called before the first frame update
    void Start()
    {
        _cinemachineBrain = GameObject.Find("MainCamera").GetComponent<CinemachineBrain>();
        _gameOver= GameObject.Find("GameOver").GetComponent<CanvasGroup>();
        _flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        _lifeCount = GameObject.Find("LifeCount").GetComponent<Text>();

        _gameOverSE = GameObject.Find("SE_hidan(CriAtomSource)").GetComponent<CriAtomSource>();

        if(GameObject.Find("Data")!=null) data = GameObject.Find("Data").GetComponent<Data>();

        _cameraBlend = _cinemachineBrain.m_DefaultBlend.m_Time;
        _SEPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        _lifeCount.text = " " + RemainingLife.life;

        //コライダーを非表示にする(即死ゾーンの重複を避けるため)
        if (_flagManager.deadZoneCol)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _flagManager.deadZoneCol = true;
            //カメラのブレンド速度を変化させる
            _cinemachineBrain.m_DefaultBlend.m_Time = 0.7f;
            //カメラをぐるりんに近づける
            vCam.SetActive(true);
            NeoConfig.isSoundFade = true;
            CriAtom.SetCategoryVolume("BGM", 0.0f);
            var _rb2d = other.GetComponent<Rigidbody2D>();
            //ぐるりんの動き(操作系・重力系)を停止
            _rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            //顔を驚き顔に変更
            _flagManager.surprise_Face = true;
            // 残機を減らす
            RemainingLife.life -= 1;
            
            if(RemainingLife.life != 0)
            {
                if (_SEPlay == false)
                {
                    _flagManager.returnGravity = true;
                    _gameOverSE.Play();
                    _SEPlay = true;
                }
            }
            else if(RemainingLife.life == 0)
            {
                _flagManager.returnGravity = true;
                //残機が0ならGameOverを表示
                _gameOver.alpha = 1.0f;

                //ゲームオーバー時のBGMを鳴らす
                if (_SEPlay == false)
                {
                    _gameOverSE.Play();
                    _SEPlay = true;
                }
            }

            if (deadZone != null) deadZone.BossSceneReload();
            Invoke("GameOver", 2.0f);
        }
    }    

    void GameOver()
    {
        NeoConfig.isSoundFade = false;
        if (RemainingLife.life != 0)
        {
            //シーンをリセット
            SceneManager.LoadScene(reloadScene);
        }
        else if(RemainingLife.life == 0)
        {
            //データを使わないときは消しておかないとタイトルに戻らない
            //data.destroy = true;
            SceneManager.LoadScene("Title");
        }
    }
}
