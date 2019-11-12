using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonGimmick : MonoBehaviour
{
    public WatchGimick watch;
    public GearGimmick gear;
    [SerializeField]private int hoursGOAL;
    [SerializeField] private int minsGOAL;
    [SerializeField] private bool trigger;
    [SerializeField] private float moveFloorDestination;
    [SerializeField] private GameObject[] moveFloors;
    [SerializeField] private float speed;
    [SerializeField] private GameObject Pigeon;
    private int direction;
    [SerializeField] private GameObject nextCamera;

    private int pigeonPattern = 0;
    [SerializeField] private float pigeonRorateSpeed;
    [SerializeField] private float pigeonMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if(moveFloorDestination < 0)
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
        PigeonAction();
    }

    private void TriggerOn()
    {
        if(watch.hours == hoursGOAL && watch.minminutes == minsGOAL && trigger == false && watch.canRotate == true)
        {
            StartCoroutine(PigeonEvent());
        }
    }

    private IEnumerator PigeonEvent()
    {
        
        watch.canRotate = false;
        if (nextCamera != null) nextCamera.SetActive(true);
        yield return new WaitForSeconds(1f);
        trigger = true;
        pigeonPattern = 1;
        while (direction != 0)
        {
            yield return null;
        }
        pigeonPattern = 2;
        yield return new WaitForSeconds(2f);
        pigeonPattern = 3;
        if (nextCamera != null) nextCamera.SetActive(false);
        yield break;
    }

    private void PigeonAction()
    {
        if (trigger)
        {
            switch (pigeonPattern)
            {
                case 1:
                    if(Pigeon.activeSelf == false) Pigeon.SetActive(true); 
                    break;
                case 2:
                    Pigeon.transform.Rotate(0, 0, pigeonRorateSpeed * Time.deltaTime);
                    Pigeon.transform.localPosition += Vector3.up * pigeonMoveSpeed * Time.deltaTime;
                    break;
                case 3:
                    if (Pigeon.activeSelf) Pigeon.SetActive(false);
                    break;
            }
        }
    }

    private void FloorsMove()
    {
        if(trigger == true)
        {
            if(direction == 1)
            {
                if(moveFloors[0].transform.localPosition.x < moveFloorDestination)
                {
                    for(int i = 0; i < moveFloors.Length; i++)
                    {
                        moveFloors[i].transform.Translate(speed * Time.deltaTime, 0, 0);
                    }
                }
                else
                {
                    for (int i = 0; i < moveFloors.Length; i++)
                    {
                        moveFloors[i].transform.localPosition = new Vector3(moveFloorDestination, moveFloors[i].transform.localPosition.y,0);
                    }
                    direction = 0;
                }
            }
            else if(direction == -1)
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
    }
}
