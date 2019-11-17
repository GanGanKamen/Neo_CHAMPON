using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerFixed : MonoBehaviour
{
    private Configuration config;
    Scrollbar fixedScrollbar;
    public int steps;
    
    // Start is called before the first frame update
    void Start()
    {
        config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();
        fixedScrollbar = GetComponent<Scrollbar>();

        //スクロールバーの現在値の設定
        fixedScrollbar.value = 0.0f;
        steps = 0;
        config.controllerfixed = steps;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Method()
    {
        if (fixedScrollbar.value <= 0.5f)
        {
            steps = 0;
        }
        else
        {
            steps = 1;
        }

        config.controllerfixed = steps;
    }
    public void OnClick()
    {
        fixedScrollbar.value = 0.0f;
        steps = 0;
        config.controllerfixed = steps;
    }
}
