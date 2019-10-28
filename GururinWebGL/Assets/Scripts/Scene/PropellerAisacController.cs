using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プロペラステージ1BGMのAISAC制御
/// </summary>


public class PropellerAisacController : MonoBehaviour
{

    private CriAtomSource source;
    private string aisacControllerName_B = "piopellerBGM_B";
    private string aisacControllerName_C = "piopellerBGM_C";
    private string aisacControllerName_D = "piopellerBGM_D";
    //public bool[] _playLimit;
    public float[] currentControlValue;
    [SerializeField] RotationCounter[] rotationCounter;
    private Waypoint _waypoint;

    private void Awake()
    {
        source = GetComponent<CriAtomSource>();
        _waypoint = GameObject.Find("Waypoint").GetComponent<Waypoint>();

        //AISACのコントロール値を0.0fにする
        currentControlValue[0] = 0.0f;
        currentControlValue[1] = 0.0f;
        currentControlValue[2] = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        source.SetAisacControl(aisacControllerName_B, currentControlValue[0]);
        source.SetAisacControl(aisacControllerName_C, currentControlValue[1]);
        source.SetAisacControl(aisacControllerName_D, currentControlValue[2]);
        //ステージ開始時にBGMを鳴らす
        //source.Play();


        //中間地点からスタートしたとき
        if (RemainingLife.waypoint)
        {
            currentControlValue[0] += 1.0f;
            if (1.0f < currentControlValue[0]) currentControlValue[0] = 1.0f;
            source.SetAisacControl(aisacControllerName_B, currentControlValue[0]);

            currentControlValue[1] += 1.0f;
            if (1.0f < currentControlValue[1]) currentControlValue[1] = 1.0f;
            source.SetAisacControl(aisacControllerName_C, currentControlValue[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (source == null) return;

        //countが加算された時AISACのコントロール値を上げる
        if (rotationCounter[0].countPlus)
        {
            currentControlValue[0] += 0.02f;
            if (1.0f < currentControlValue[0]) currentControlValue[0] = 1.0f;
            source.SetAisacControl(aisacControllerName_B, currentControlValue[0]);
        }

        //中間地点を切った時AISACのコントロール値を上げる
        if (_waypoint.animPlay)
        {
            currentControlValue[1] += 0.02f;
            if (1.0f < currentControlValue[1]) currentControlValue[1] = 1.0f;
            source.SetAisacControl(aisacControllerName_C, currentControlValue[1]);
        }

        if (rotationCounter[1].countPlus)
        {
            currentControlValue[2] += 0.02f;
            if (1.0f < currentControlValue[2]) currentControlValue[2] = 1.0f;
            source.SetAisacControl(aisacControllerName_D, currentControlValue[2]);
        }
    }
}
