using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    static public void PlayS(GameObject soundObj, string soundName)
    {
        soundObj.GetComponent<CriAtomSource>().Play(soundName);
    }

    static public void PlayS(GameObject soundObj, string soundName,bool loop)
    {
        soundObj.GetComponent<CriAtomSource>().loop = loop;
       soundObj.GetComponent<CriAtomSource>().Play(soundName);
    }

    static public void PlayS(GameObject soundObj)
    {
        soundObj.GetComponent<CriAtomSource>().Play();
    }

    static public void PlayS(GameObject soundObj, string cuesheet, string cuename)
    {
        soundObj.GetComponent<CriAtomSource>().cueSheet = cuesheet;
        soundObj.GetComponent<CriAtomSource>().Play(cuename);
    }

    static public void PlayS(GameObject soundObj, string soundName,int num) //CriAtomSourceコンポーネントが複数ある場合 numは0から
    {
        CriAtomSource[] atomSources = soundObj.GetComponents<CriAtomSource>();
        atomSources[num].Play(soundName);
    }
    static public void PlayS(GameObject soundObj, string soundName, int num,bool loop) //CriAtomSourceコンポーネントが複数ある場合 numは0から
    {
        CriAtomSource[] atomSources = soundObj.GetComponents<CriAtomSource>();
        atomSources[num].loop = loop;
        atomSources[num].Play(soundName);
    }
    static public void PlayS(GameObject soundObj, int num)
    {
        CriAtomSource[] atomSources = soundObj.GetComponents<CriAtomSource>();
        atomSources[num].Play();
    }

    static public void StopS(GameObject soundObj)
    {
        soundObj.GetComponent<CriAtomSource>().Stop();
    }

    static public void StopS(GameObject soundObj,int num)
    {
        CriAtomSource[] atomSources = soundObj.GetComponents<CriAtomSource>();
        atomSources[num].Stop();
    }
    static public void MuteOrPlay(GameObject soundObj, bool PlayorMute)
    {
        CriAtomSource atomSources = soundObj.GetComponent<CriAtomSource>();
        atomSources.playOnStart = true;
        atomSources.loop = true;
        switch (PlayorMute)
        {
            case true:
                atomSources.volume = 1;
                break;
            case false:
                atomSources.volume = 0;
                break;
        }
    }
    static public void MuteOrPlay(GameObject soundObj,bool PlayorMute,int num)
    {
        CriAtomSource[] atomSources = soundObj.GetComponents<CriAtomSource>();
        atomSources[num].playOnStart = true;
        atomSources[num].loop = true;
        switch (PlayorMute)
        {
            case true:
                atomSources[num].volume = 1;
                break;
            case false:
                atomSources[num].volume = 0;
                break;
        }
    }

    static public void PlayOrStop(GameObject soundObj)
    {
        CriAtomSource atomSource = soundObj.GetComponent<CriAtomSource>();
        if(atomSource.status == CriAtomSource.Status.Playing)
        {
            atomSource.Stop();
        }
        else if(atomSource.status == CriAtomSource.Status.Stop)
        {
            atomSource.Play();
        }
    }
}
