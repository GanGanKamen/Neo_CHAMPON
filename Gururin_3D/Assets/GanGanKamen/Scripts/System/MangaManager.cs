using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace GanGanKamen
{
    public class MangaManager : MonoBehaviour
    {
        private PlayableDirector mangaDirector;
        private bool playOver = false;

        private void Awake()
        {
            mangaDirector = GetComponent<PlayableDirector>();
        }

        void Start()
        {
            mangaDirector.Play();
            
        }

        // Update is called once per frame
        void Update()
        {
            if(playOver == false && mangaDirector.state == PlayState.Paused)
            {
                playOver = true;
                Fader.FadeIn(5f, "StageSelect");
            }
        }
    }
}

