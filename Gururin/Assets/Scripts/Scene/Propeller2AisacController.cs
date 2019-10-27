using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プロペラステージ2BGMのAISAC制御
/// </summary>


public class Propeller2AisacController : MonoBehaviour
{

    private CriAtomSource source;
    private string aisacControllerName_B = "piopellerBGM_B";
    private string aisacControllerName_C = "piopellerBGM_C";
    private string aisacControllerName_D = "piopellerBGM_D";
    //public bool[] _playLimit;
    public float[] currentControlValue;
    [SerializeField] BlockSwitch[] blockSwitches;
    [SerializeField] GlassSwitch glassSwitch;

    private void Awake()
    {
        source = GetComponent<CriAtomSource>();

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
    }

    // Update is called once per frame
    void Update()
    {
        if (source == null) return;

        //スイッチが押されたときAISACのコントロール値を上げる
        if (blockSwitches[0].blocking)
        {
            currentControlValue[0] += 0.05f;
            if (1.0f < currentControlValue[0]) currentControlValue[0] = 1.0f;
            source.SetAisacControl(aisacControllerName_B, currentControlValue[0]);
        }
    
        if (blockSwitches[1].blocking)
        {
            currentControlValue[1] += 0.05f;
            if (1.0f < currentControlValue[1]) currentControlValue[1] = 1.0f;
            source.SetAisacControl(aisacControllerName_C, currentControlValue[1]);
        }
    
        if (glassSwitch.blocking)
        {
            currentControlValue[2] += 0.05f;
            if (1.0f < currentControlValue[2]) currentControlValue[2] = 1.0f;
            source.SetAisacControl(aisacControllerName_D, currentControlValue[2]);
        }
    }
}
