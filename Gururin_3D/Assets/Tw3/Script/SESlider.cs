using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SESlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    Slider seSlider;

    // Start is called before the first frame update
    void Start()
    {
        seSlider = GetComponent<Slider>();

        //スライダーの最大値の設定
        seSlider.maxValue = 0.0f;

        //スライダーの最小値の設定
        seSlider.minValue = -80.0f;

        //スライダーの現在値の設定
        seSlider.value = -40.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Method()
    {
        audioMixer.SetFloat("SE", seSlider.value);
    }

    public void OnClick()
    {
        seSlider.value = -40.0f;
        audioMixer.SetFloat("SE", seSlider.value);
    }
}
