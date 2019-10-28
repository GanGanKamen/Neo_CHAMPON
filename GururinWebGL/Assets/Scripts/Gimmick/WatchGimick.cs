using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchGimick : MonoBehaviour
{
    public GameObject pointer1, pointer2;
    [Range(-1, 1)] public int direction;//0静止、1時計回り、-1逆時計 
    [SerializeField] [Range(0, 200)] private float speed;
    [Range(1, 12)] public int hours;
    [Range(0, 59)] public int minminutes;
    public bool canRotate;

    // Start is called before the first frame update
    void Start()
    {
        canRotate = true;
        if (minminutes >= 0 && minminutes < 15)
        {
            pointer1.transform.localEulerAngles = new Vector3(0, 0, 90 - minminutes * 6);
        }
        else if (minminutes == 15)
        {
            pointer1.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            pointer1.transform.localEulerAngles = new Vector3(0, 0, 360 - (minminutes / 15f - 1f) * 90);
        }

        if (hours >= 1 && hours < 3)
        {
            pointer2.transform.localEulerAngles = new Vector3(0, 0, 90 - hours * 30 - minminutes * 0.5f);
        }
        else if (hours == 3)
        {
            pointer2.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            pointer2.transform.localEulerAngles = new Vector3(0, 0, 360 - (hours / 3f - 1f) * 90 - minminutes * 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TestKeyCtrl();
        MinminutesChange();
    }


    public void PointerRotate(bool PlusOrMinus)
    {
        if (canRotate == false) return;
        if (PlusOrMinus == true)
        {
            pointer1.transform.Rotate(new Vector3(0, 0, 1), -speed * Time.deltaTime);
            pointer2.transform.Rotate(new Vector3(0, 0, 1), -speed / 12f * Time.deltaTime);
            direction = 1;
        }
        else
        {
            pointer1.transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
            pointer2.transform.Rotate(new Vector3(0, 0, 1), speed / 12f * Time.deltaTime);
            direction = -1;
        }
    }

    public int Pointer1Num()
    {
        int num = (int)((360f - pointer1.transform.localEulerAngles.z) / 6f) + 15;
        if (num < 60)
        {
            return num;
        }
        else
        {
            return num - 60;
        }
    }

    private void HourChange(bool PulsOrMinus)
    {
        switch (PulsOrMinus)
        {
            case true:
                if (hours < 12)
                {
                    hours += 1;
                }
                else
                {
                    hours = 1;
                }
                break;
            case false:
                if (hours > 1)
                {
                    hours -= 1;
                }
                else
                {
                    hours = 12;
                }
                break;
        }
    }

    private void MinminutesChange()
    {
        if (minminutes != Pointer1Num())
        {
            if (direction == 1 && Pointer1Num() < minminutes)
            {
                minminutes = 0;
                HourChange(true);
            }
            else if (direction == -1 && Pointer1Num() > minminutes)
            {
                minminutes = 59;
                HourChange(false);
            }
            else
            {
                if (minminutes > Pointer1Num())
                {
                    minminutes--;
                }
                else if (minminutes < Pointer1Num())
                {
                    minminutes++;
                }
            }

        }
    }

    private void TestKeyCtrl()
    {
        if (Input.GetKey(KeyCode.S))
        {
            PointerRotate(true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            PointerRotate(false);
        }
    }
}
