using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickDistance : MonoBehaviour
{
    private Configuration config;
    Slider disSlider;

    // Start is called before the first frame update
    void Start()
    {
        config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();
        disSlider = GetComponent<Slider>();

        //スライダーの最大値の設定
        disSlider.maxValue = 0.2f;

        //スライダーの最小値の設定
        disSlider.minValue = 0.0f;

        //スライダーの現在値の設定
        disSlider.value = 0.1f;

        config.flickdistance = disSlider.value;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Method()
    {
        config.flickdistance = disSlider.value;

        Debug.Log("FlickDistance：" + disSlider.value);
        
    }
    public void OnClick()
    {
        disSlider.value = 0.1f;

        config.flickdistance = disSlider.value;
    }
}
