using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    [SerializeField]private Configuration config;
    Slider senSlider;

    // Start is called before the first frame update
    void Start()
    {
        //config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();
        senSlider = GetComponent<Slider>();
        
        //スライダーの最大値の設定
        senSlider.maxValue = 2.0f;

        //スライダーの最小値の設定
        senSlider.minValue = 1.0f;

        //スライダーの現在値の設定
        senSlider.value = 1.5f;

        config.sensitivity = senSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Method()
    {
        config.sensitivity = senSlider.value;

        Debug.Log("Sensitivity：" + senSlider.value);
        
    }
    public void OnClick()
    {
        senSlider.value = 1.5f;

        config.sensitivity = senSlider.value;
    }
}
