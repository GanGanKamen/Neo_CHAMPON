using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void FadeOut(float time)
    {
        GameObject fader = Instantiate(Resources.Load<GameObject>("FadeCanvas"));
        fader.GetComponent<FadeCanvas>().FadeOut(time);
    }

    static public void FadeIn(float time,string sceneName)
    {
        GameObject fader = Instantiate(Resources.Load<GameObject>("FadeCanvas"));
        fader.GetComponent<FadeCanvas>().FadeIn(time,sceneName);
    }

    static public void FadeInBlack(float time, string sceneName)
    {
        GameObject fader = Instantiate(Resources.Load<GameObject>("FadeCanvasBlack"));
        fader.GetComponent<FadeCanvas>().FadeIn(time, sceneName);
    }
}
