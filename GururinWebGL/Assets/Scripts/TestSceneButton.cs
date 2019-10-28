using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneButton : MonoBehaviour
{
    private GameObject[] targetObjs;
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
        targetObjs = new GameObject[2];
        targetObjs[0] = GameObject.Find("ConfigCanvas");
        targetObjs[1] = GameObject.Find("FlagManager");
        for(int i = 0; i < targetObjs.Length; i++)
        {
            Destroy(targetObjs[i]);
        }
    }
}
