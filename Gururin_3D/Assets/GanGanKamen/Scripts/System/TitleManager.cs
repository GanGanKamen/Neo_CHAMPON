using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GanGanKamen
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private Button StartButton;
        // Start is called before the first frame update
        private void Awake()
        {
            StartButton.onClick.AddListener(() => GameStart());
        }

        void Start()
        {
            GanGanKamen.GameSystem.StartUpCount += 1;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void GameStart()
        {
            Fader.FadeIn(1f, "StageSelect");
            /*
            if (GanGanKamen.GameStart.StartUpCount <= 1) Fader.FadeIn(2f, "Manga");
            else Fader.FadeIn(1f, "StageSelect");
            */
        }
    }
}

