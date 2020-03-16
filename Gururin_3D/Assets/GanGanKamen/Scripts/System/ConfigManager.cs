using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GanGanKamen
{
    public class ConfigManager : MonoBehaviour
    {
        [SerializeField] private Button menubutton;
        [SerializeField] private Button menuWindowCloseNutton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button backStageSelectButton;
        [SerializeField] private Button backTitleButton;
        [SerializeField] private GameObject menuWindow;
        [SerializeField] private string[] unappScenes;
        [SerializeField] private string[] notStageScenes;

        private void Awake()
        {
            menubutton.onClick.AddListener(() => MenuOpen());
            menuWindowCloseNutton.onClick.AddListener(() => MenuClose());
            backTitleButton.onClick.AddListener(() => BackToTitle());
            retryButton.onClick.AddListener(() => Retry());
            backStageSelectButton.onClick.AddListener(() => BackToStageSelect());
        }

        // Start is called before the first frame update
        void Start()
        {
            MenuInit();
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            GetSceneChange();
        }

        private void MenuOpen()
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                if (player.CanCtrl == false) return;
            }
            menuWindow.SetActive(true);
            menuWindowCloseNutton.gameObject.SetActive(true);
            menubutton.gameObject.SetActive(false);
            if(GameObject.FindGameObjectWithTag("Player") != null)
            {
                var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                player.ProhibitControll();
            }
        }

        private void MenuClose()
        {
            menuWindow.SetActive(false);
            menuWindowCloseNutton.gameObject.SetActive(false);
            menubutton.gameObject.SetActive(true);
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                player.PermitControll();
            }
        }

        private void MenuInit()
        {
            menuWindow.SetActive(false);
            menuWindowCloseNutton.gameObject.SetActive(false);
            menubutton.gameObject.SetActive(true);
        }

        private void BackToTitle()
        {
            MenuInit();
            Fader.FadeIn(2f, "Title");
        }

        private void BackToStageSelect()
        {
            MenuInit();
            Fader.FadeInBlack(2f, "StageSelect");
        }

        private void Retry()
        {
            MenuInit();
            Fader.FadeInBlack(2f, GameSystem.nowSceneName);
        }

        private void GetSceneChange()
        {
            if (GameSystem.isSceneChange)
            {
                MenuInit();
                for(int i = 0; i < notStageScenes.Length; i++)
                {
                    if (GameSystem.nowSceneName == notStageScenes[i])
                    {
                        retryButton.interactable = false;
                        backStageSelectButton.interactable = false;
                        backTitleButton.interactable = false;
                        break;
                    }
                    else
                    {
                        /*
                        retryButton.gameObject.SetActive(true);
                        backStageSelectButton.gameObject.SetActive(true);
                        backTitleButton.gameObject.SetActive(true);
                        */
                        retryButton.interactable = true;
                        backStageSelectButton.interactable = true;
                        backTitleButton.interactable = true;
                    }
                }
               
                int count = 0;
                for(int i = 0; i < unappScenes.Length; i++)
                {
                    if(GameSystem.nowSceneName == unappScenes[i])
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

