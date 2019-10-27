using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFadeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onclick(string name)
    {
        SoundManager.PlayS(gameObject);
        Fader.FadeIn(2f, name);
    }
}
