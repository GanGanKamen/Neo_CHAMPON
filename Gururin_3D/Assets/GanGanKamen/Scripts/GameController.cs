using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float InputAngle { get { return inputAngle; } }
    public bool InputFlick { get { return InputFlickCheck(); } }
    public bool InputLongPress { get { return InputLongPressCheck(); } }

    [SerializeField] private GameObject controllerObject;
    [SerializeField] private GameObject controller;
    [SerializeField] private float jumpInterval;
    [SerializeField] private float flickDistance;
    [SerializeField] private Vector2 poslimit;
    [SerializeField] private float longPressInterval;

    private Vector2 mousePosition1, prepos, pos;
    private Vector2 mousePosition2, mousePosition3, mousePosition4, mousePosition5;
    private float jumpcount = 0;
    private float angle = 0;
    private float preEulerAngle = 0;
    private float inputAngle = 0;
    private Vector3 AxB = Vector3.zero;
    private bool isPress = false;
    private int flickCountPre = 0;
    private int flickCount = 0;
    private bool isTouch = false;
    private int timercount = 0;
    private Platform platform;
    private float longPressCount = 0;


    private void Awake()
    {
        preEulerAngle = controller.transform.localEulerAngles.z;
        GetPlatform();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Controll();
        InputAngleUpdate();
    }

    private void Controll()
    {
        switch (platform)
        {
            case Platform.Windows:
                WindowsCtrl();
                break;
            default:

                break;
        }
    }

    private void WindowsCtrl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            jumpcount = 0;
            timercount = 0;
            longPressCount = 0;
            controllerObject.SetActive(true);
            controller.transform.rotation = Quaternion.identity;
            mousePosition1 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>()
                .ScreenToViewportPoint(Input.mousePosition);
            mousePosition1.y = mousePosition1.y - 0.1f;
            if (mousePosition1.x < poslimit.x)
            {
                mousePosition1.x = poslimit.x;
            }
            else if (mousePosition1.x > 1 - poslimit.x)
            {
                mousePosition1.x = 1 - poslimit.x;
            }

            if (mousePosition1.y < poslimit.y)
            {
                mousePosition1.y = poslimit.y;
            }
            else if (mousePosition1.y > 1 - poslimit.y)
            {
                mousePosition1.y = 1 - poslimit.y;
            }
            controllerObject.transform.position =
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ViewportToScreenPoint(mousePosition1);
            prepos = controllerObject.transform.position;
            isPress = true;
        }
        if (Input.GetMouseButton(0) && isPress)
        {
            jumpcount += Time.deltaTime;
            timercount++;
            mousePosition2 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().
                ScreenToViewportPoint(Input.mousePosition);
            pos = mousePosition1;
            var vecA = prepos - pos;
            var vecB = mousePosition2 - pos;
            if(timercount >= 10)
            {
                angle = Vector3.Angle(vecA, vecB);
                Debug.Log(angle);
                AxB = Vector3.Cross(vecA, vecB);
            }
            
            if(angle <= 0.5f)
            {
                longPressCount += Time.deltaTime;
            }
            else
            {
                longPressCount = 0;
            }

            //外積が正の時の処理
            if (AxB.z > 0)
            {
                //controller.transform.rotation = controller.transform.rotation * Quaternion.Euler(0, 0, angle);
                controller.transform.localEulerAngles += new Vector3(0, 0, angle);
            }
            //外積が負の時の処理
            else if (AxB.z < 0)
            {
                //controller.transform.rotation = controller.transform.rotation * Quaternion.Euler(0, 0, -angle);
                controller.transform.localEulerAngles -= new Vector3(0, 0, angle);
            }
            //preposにこの時点でのマウス位置を代入 camerapos2にこの時点でのカメラ位置を再代入
            prepos = mousePosition2;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePosition5 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().
                ScreenToViewportPoint(Input.mousePosition);
            var flickDis = Vector2.Distance(mousePosition5, pos);
            if (jumpcount <= jumpInterval && flickDis >= flickDistance)
            {
                flickCount += 1;
            }
            isPress = false;
            angle = 0;
            jumpcount = 0;
            longPressCount = 0;
            controllerObject.SetActive(false);
        }
    }

    private void InputAngleUpdate()
    {
        if(preEulerAngle != controller.transform.localEulerAngles.z)
        {
            if (AxB.z > 0)
            {
                inputAngle = angle;
            }
            else if(AxB.z < 0)
            {
                inputAngle = -angle;
            }
            preEulerAngle = controller.transform.localEulerAngles.z;
        }
        else
        {
            inputAngle = 0;
        }
    }

    private bool InputFlickCheck()
    {
        if(flickCount != flickCountPre)
        {
            flickCountPre = flickCount;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool InputLongPressCheck()
    {
        if(longPressCount >= longPressInterval)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GetPlatform()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            platform = Platform.Android;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            platform = Platform.iOS;
        }
        else
        {
            platform = Platform.Windows;
        }
    }
}
