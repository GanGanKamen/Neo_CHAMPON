using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReset : MonoBehaviour
{
    [SerializeField] BGMSlider bgmSlider;
    [SerializeField] SESlider seSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        bgmSlider.OnClick();
        seSlider.OnClick();
    }
}
