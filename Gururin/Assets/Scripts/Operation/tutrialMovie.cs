using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class tutrialMovie : MonoBehaviour
{
    [SerializeField] VideoPlayer video;
    [SerializeField] GameObject videoStop;
    // Start is called before the first frame update
    void Start()
    {
        video.Prepare();
    }

    // Update is called once per frame
    void Update()
    {
        if (video.isPlaying)
        {
            videoStop.SetActive(false);
        }
        else
        {
            videoStop.SetActive(true);
        }
    }
}
