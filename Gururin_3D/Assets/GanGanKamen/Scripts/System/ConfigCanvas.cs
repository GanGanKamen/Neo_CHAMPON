using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GanGanKamen
{
    public class ConfigCanvas : MonoBehaviour
    {
        [SerializeField] private Button menubutton;
        [SerializeField] private Button menuWindowCloseNutton;
        [SerializeField] private Button backTitleButton;
        [SerializeField] private GameObject menuWindow;
        [SerializeField] private string[] ignoreScenes;

        private void Awake()
        {
            menubutton.onClick.AddListener(() => MenuOpen());
            menuWindowCloseNutton.onClick.AddListener(() => MenuClose());
            backTitleButton.onClick.AddListener(() => BackToTitle());
        }

        // Start is called before the first frame update
        void Start()
        {
            MenuClose();
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            GetSceneChange();
        }

        private void MenuOpen()
        {
            menuWindow.SetActive(true);
            menubutton.gameObject.SetActive(false);
        }

        private void MenuClose()
        {
            menuWindow.SetActive(false);
            menubutton.gameObject.SetActive(true);
        }

        private void BackToTitle()
        {
            MenuClose();
            Fader.FadeIn(2f, "Title");
        }

        private void GetSceneChange()
        {
            if (GameSystem.isSceneChange)
            {
                MenuClose();
                if(GameSystem.nowSceneName == "Title")
                {
                    backTitleButton.gameObject.SetActive(false);
                }
                else
                {
                    backTitleButton.gameObject.SetActive(true);
                }
                int count = 0;
                for(int i = 0; i < ignoreScenes.Length; i++)
                {
                    if(GameSystem.nowSceneName == ignoreScenes[i])
                    {
                        count += 1;
                        break;
                    }
                }
                if(count > 0)
                {
                    menubutton.gameObject.SetActive(false);
                }
            }
        }
    }
}

