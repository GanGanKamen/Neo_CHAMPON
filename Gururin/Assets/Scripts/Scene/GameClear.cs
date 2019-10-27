using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ステージクリア時のSE制御
/// </summary>

public class GameClear : MonoBehaviour
{

    private CanvasGroup _stageClear;
    private CriAtomSource _goalSE;

    public bool[] playSE;

    //Data用のbool
    public bool goal;

    // Start is called before the first frame update
    void Start()
    {
        _stageClear = GameObject.Find("StageClear").GetComponent<CanvasGroup>();
        _goalSE = GetComponent<CriAtomSource>();

        for(int i = 0; i < playSE.Length; i++)
        {
            playSE[i] = false;
        }
        goal = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _stageClear.alpha = 1.0f;
            RemainingLife.waypoint = false;
            playSE[0] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //一度だけゴールSEを鳴らす
        if(playSE[0] && playSE[1] == false)
        {
            _goalSE.Play();
            goal = true;
            playSE[1] = true;
        }
    }
}
