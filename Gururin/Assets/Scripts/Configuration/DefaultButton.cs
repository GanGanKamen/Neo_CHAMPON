using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultButton : MonoBehaviour
{
    public Sensitivity sensitivity;
    public ControllerFixed controllerFixed;
    public ControllerPosition controllerPosition;
    public NeoConfig NeoConfig;

    // Start is called before the first frame update
    void Start()
    {
        if(sensitivity == null)
        {
            sensitivity = GameObject.Find("Sensitivity").GetComponent<Sensitivity>();
        }
        if(controllerFixed == null)
        {
            controllerFixed = GameObject.Find("ControllerFixed").GetComponent<ControllerFixed>();
        }

        if(controllerPosition == null)
        {
            controllerPosition = GameObject.Find("ControllerPosition").GetComponent<ControllerPosition>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        sensitivity.OnClick();
        controllerFixed.OnClick();
        controllerPosition.OnClick();
        NeoConfig.BGMSlider.value = 8;
        NeoConfig.SESlider.value = 8;
        NeoConfig.textSpeedSlider.value = 0;
        NeoConfig.touchSlider.value = 1;
    }
}
