using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BGMSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    Slider bgmSlider;

    // Start is called before the first frame update
    void Start()
    {
        bgmSlider = GetComponent<Slider>();

        //スライダーの最大値の設定
        bgmSlider.maxValue = 0.0f;

        //スライダーの最小値の設定
        bgmSlider.minValue = -80.0f;

        //スライダーの現在値の設定
        bgmSlider.value = -40.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Method()
    {
        audioMixer.SetFloat("BGM", bgmSlider.value);
    }

    public void OnClick()
    {
        bgmSlider.value = -40.0f;
        audioMixer.SetFloat("BGM", bgmSlider.value);
    }
}
