using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GanGanKamen
{
    public class ResultManagaer : MonoBehaviour
    {
        [SerializeField] GameObject[] flowers;
        [SerializeField] Button nextButton;
        [SerializeField] Button retryButton;
        [SerializeField] Button overButton;
        // Start is called before the first frame update
        public void Init(StageManager stageManager)
        {
            var assessment = 0;
            if (stageManager.ElapsedTime <= stageManager.ClearTimeGoal)
            {
                flowers[0].SetActive(true);
                assessment += 1;
            }
            if(stageManager.ItemNum >= stageManager.AllItem)
            {
                flowers[1].SetActive(true);
                assessment += 1;
            }
            if (stageManager.Medal)
            {
                flowers[2].SetActive(true);
                assessment += 1;
            }
            stageManager.saveData.Save(stageManager.NowStageNumber,assessment);
            nextButton.onClick.AddListener(() => NextButton());
            retryButton.onClick.AddListener(() => RetryButton());
            overButton.onClick.AddListener(() => OverButton());
        }

        private void NextButton()
        {
            Fader.FadeIn(3f, "StageSelect");
        }

        private void RetryButton()
        {
            Fader.FadeInBlack(2f, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        private void OverButton()
        {
            Fader.FadeInBlack(3f, "Title");
        }
    }
}


