using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WatchBoss
{
    public class BossPigeon : MonoBehaviour
    {
        public WatchGimick watch;
        public GearGimmick gear;
        [SerializeField] private int hoursGOAL;
        [SerializeField] private int minsGOAL;
        [SerializeField] private bool trigger;
        [SerializeField] private float moveFloorDestination;
        [SerializeField] private GameObject[] moveFloors;
        [SerializeField] private float speed;
        [SerializeField] private GameObject Pigeon;
        private int direction;
        private bool pigeonReturn = false;
        // Start is called before the first frame update
        void Start()
        {
            if (moveFloorDestination < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
        }

        // Update is called once per frame
        void Update()
        {
            TriggerOn();
            FloorsMove();
        }

        private void TriggerOn()
        {
            if (watch.hours == hoursGOAL && watch.minminutes == minsGOAL && trigger == false && watch.canRotate == true)
            {
                StartCoroutine(PigeonEvent());
            }
        }

        private IEnumerator PigeonEvent()
        {

            watch.canRotate = false;
            trigger = true;
            Pigeon.SetActive(true);
            while (direction != 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(2f);
            pigeonReturn = true;
            if (moveFloorDestination < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
            while (trigger == true)
            {
                yield return null;
            }
            pigeonReturn = false;
            watch.canRotate = true;
            Pigeon.SetActive(false);
            yield break;
        }

        private void FloorsMove()
        {
            if (trigger == true)
            {
                if (pigeonReturn == false)
                {
                    if (direction == 1)
                    {
                        if (moveFloors[0].transform.localPosition.x < moveFloorDestination)
                        {
                            for (int i = 0; i < moveFloors.Length; i++)
                            {
                                moveFloors[i].transform.Translate(speed * Time.deltaTime, 0, 0);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < moveFloors.Length; i++)
                            {
                                moveFloors[i].transform.localPosition = new Vector3(moveFloorDestination, moveFloors[i].transform.localPosition.y, 0);
                            }
                            direction = 0;
                        }
                    }
                    else if (direction == -1)
                    {
                        if (moveFloors[0].transform.localPosition.x > moveFloorDestination)
                        {
                            for (int i = 0; i < moveFloors.Length; i++)
                            {
                                moveFloors[i].transform.Translate(-speed * Time.deltaTime, 0, 0);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < moveFloors.Length; i++)
                            {
                                moveFloors[i].transform.localPosition = new Vector3(moveFloorDestination, moveFloors[i].transform.localPosition.y, 0);
                            }
                            direction = 0;
                        }
                    }
                }

                else
                {
                    if (direction == 1)
                    {
                        if (moveFloors[0].transform.localPosition.x > 0)
                        {
                            for (int i = 0; i < moveFloors.Length; i++)
                            {
                                moveFloors[i].transform.Translate(-speed * Time.deltaTime, 0, 0);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < moveFloors.Length; i++)
                            {
                                moveFloors[i].transform.localPosition = new Vector3(0, moveFloors[i].transform.localPosition.y, 0);
                            }
                            trigger = false;
                        }
                    }
                    else if (direction == -1)
                    {
                        if (moveFloors[0].transform.localPosition.x < 0)
                        {
                            for (int i = 0; i < moveFloors.Length; i++)
                            {
                                moveFloors[i].transform.Translate(speed * Time.deltaTime, 0, 0);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < moveFloors.Length; i++)
                            {
                                moveFloors[i].transform.localPosition = new Vector3(0, moveFloors[i].transform.localPosition.y, 0);
                            }
                            trigger = false;
                        }
                    }
                }
            }
        }
    }
}


