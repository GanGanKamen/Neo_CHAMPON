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
        [SerializeField] float flowerMaxSize;
        [SerializeField] float flowerInSpeed;

        /*
        private void Start()
        {
            StartCoroutine(TestReviewProcess());
        }
        */
        public void Init(StageManager stageManager)
        {
            StartCoroutine(ReviewProcess(stageManager));
        }
        
        private IEnumerator ReviewProcess(StageManager stageManager)
        {
            var assessment = 0;

            nextButton.onClick.AddListener(() => NextButton());
            retryButton.onClick.AddListener(() => RetryButton());
            overButton.onClick.AddListener(() => OverButton());
            nextButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(false);
            overButton.gameObject.SetActive(false);

            for (int i = 0; i < flowers.Length; i++)
            {
                flowers[i].SetActive(false);
            }
            var assessments = new bool[3];
            for (int i = 0; i < assessments.Length; i++) assessments[i] = false;
            if (stageManager.ElapsedTime <= stageManager.ClearTimeGoal)
            {
                assessments[0] = true;
                assessment += 1;
            }
            if (stageManager.ItemNum >= 20)
            {
                assessments[1] = true;
                assessment += 1;
            }
            if (stageManager.Medal)
            {
                assessments[2] = true;
                assessment += 1;
            }

            for (int i = 0; i < assessments.Length; i++)
            {
                if (assessments[i])
                {
                    FlowerInit(i);
                    do
                    {
                        assessments[i] = FlowerIn(flowers[i].GetComponent<RectTransform>());
                        yield return null;
                    } while (assessments[i] == false);
                }
            }
            stageManager.saveData.Save(stageManager.NowStageNumber, assessment);
            nextButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            overButton.gameObject.SetActive(true);
            yield break;
        }
        

        private IEnumerator TestReviewProcess()
        {
            for (int i = 0; i < flowers.Length; i++)
            {
                flowers[i].SetActive(false);
            }
            FlowerInit(0);
            var assessment0 = true;

            do
            {
                assessment0 = FlowerIn(flowers[0].GetComponent<RectTransform>());
                yield return null;
            } while (assessment0 == false);

            yield break;
        }

        private void FlowerInit(int num)
        {
            flowers[num].SetActive(true);
            flowers[num].GetComponent<RectTransform>().localScale = Vector3.one * flowerMaxSize;
        }

        private bool FlowerIn(RectTransform rect)
        {
            if(rect.localScale.x > 1)
            {
                rect.localScale -= Vector3.one * Time.deltaTime * flowerInSpeed;
                return false;
            }
            else
            {
                rect.localScale = Vector3.one;
                return true;
            }
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


