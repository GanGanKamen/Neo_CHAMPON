using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountSlider : MonoBehaviour
{

    private Slider _chargeSlider;

    [SerializeField] RotationCounter rotationCounter;

    // Start is called before the first frame update
    void Start()
    {
        _chargeSlider = GetComponent<Slider>();
        _chargeSlider.maxValue = rotationCounter._maxCount;
    }

    // Update is called once per frame
    void Update()
    {
        //カウントをスライダーの値に設定
        _chargeSlider.value = rotationCounter.count;
    }
}
