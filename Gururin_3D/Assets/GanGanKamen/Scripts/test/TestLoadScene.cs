using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoadScene : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(canvas);
        }
    }
}
