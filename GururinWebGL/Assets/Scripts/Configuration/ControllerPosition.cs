using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPosition : MonoBehaviour
{
    [SerializeField]private Configuration config;
    [SerializeField]private Scrollbar posScrollbar;
    public int steps;

    // Start is called before the first frame update
    void Start()
    {
        //config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();

        //スクロールバーの現在値の設定
        posScrollbar.value = 0.5f;
        steps = 1;
        config.controllerposition = steps;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Method()
    {
        if (posScrollbar.value < 0.25f)
        {
            posScrollbar.value = 0.0f;
            steps = 2;
        }
        else if(posScrollbar.value >= 0.25f && posScrollbar.value < 0.75f)
        {
            posScrollbar.value = 0.5f;
            steps = 1;
        }
        else if(posScrollbar.value >= 0.75f)
        {
            posScrollbar.value = 1.0f;
            steps = 0;
        }

        config.controllerposition = steps;

        Debug.Log("Controller Position：" + steps);
        
    }
    public void OnClick()
    {
        posScrollbar.value = 0.5f;
        steps = 1;
        config.controllerposition = steps;
    }
}
