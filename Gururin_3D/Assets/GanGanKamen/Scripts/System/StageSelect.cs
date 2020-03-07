using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GanGanKamen;

namespace GanGanKamen
{
    public class StageSelect : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private RectTransform images;
        [SerializeField] private float moveSpeed;
        [SerializeField]private bool isMove = false;
        [SerializeField]private int nowArea = 1;
        [SerializeField]private int preArea = 1;
        // Start is called before the first frame update
        void Start()
        {

        }

        public void MoveButton(int nextArea)
        {
            if (nextArea > 3 || nextArea < 1 || isMove) return;
            nowArea = nextArea;
        }

        public void StartStage(string sceneName)
        {
            if (isMove) return;
            Fader.FadeIn(3f, sceneName);
        }

        private void FlickAction()
        {
            if (isMove) return;
            if (gameController.InputFlick)
            {
                switch (gameController.InputFlickDirection)
                {
                    case FlickDirection.Up:
                        if(nowArea < 3)
                        {
                            nowArea += 1;
                        }
                        break;
                    case FlickDirection.Down:
                        if(nowArea > 1)
                        {
                            nowArea -= 1;
                        }
                        break;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            FlickAction();
            SwitchArea();
        }

        private void SwitchArea()
        {
            if(nowArea != preArea)
            {
                isMove = true;
                switch (nowArea)
                {
                    case 1:
                        if(images.localPosition.y < 0)
                        {
                            images.localPosition += new Vector3(0, moveSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            images.localPosition = Vector3.zero;
                            preArea = nowArea;
                            isMove = false;
                        }
                        break;
                    case 2:
                        if(preArea > nowArea)
                        {
                            if (images.localPosition.y < -1080)
                            {
                                images.localPosition += new Vector3(0, moveSpeed * Time.deltaTime, 0);
                            }
                            else
                            {
                                images.localPosition = new Vector3(0,-1080,0);
                                preArea = nowArea;
                                isMove = false;
                            }
                        }
                        else if(preArea < nowArea)
                        {
                            if (images.localPosition.y > -1080)
                            {
                                images.localPosition -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
                            }
                            else
                            {
                                images.localPosition = new Vector3(0, -1080, 0);
                                preArea = nowArea;
                                isMove = false;
                            }
                        }
                        break;
                    case 3:
                        if (images.localPosition.y > -2160)
                        {
                            images.localPosition -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            images.localPosition = new Vector3(0, -2160, 0);
                            preArea = nowArea;
                            isMove = false;
                        }
                        break;
                }
            }
        }
    }
}

