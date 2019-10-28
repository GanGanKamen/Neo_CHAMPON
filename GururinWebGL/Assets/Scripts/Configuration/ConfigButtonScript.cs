using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigButtonScript : MonoBehaviour
{
    public Configuration config;

    // Start is called before the first frame update
    void Start()
    {
        config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (config == null)
        {
            config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();
        }*/
    }

    public void OnClick()
    {
        config.Method();
    }
}
