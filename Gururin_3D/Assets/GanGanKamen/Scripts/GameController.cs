using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float InputAngle { get { return inputAngle; } }

    [SerializeField] private GameObject controllerObject;
    [SerializeField] private GameObject controller;
    [SerializeField] private float jumpInterval;
    [SerializeField] private float flickDistance;


    private Vector2 mousePosition1, poslimit, prepos, pos, vecA, vecB;
    private Vector2 mousePosition2, mousePosition3, mousePosition4, mousePosition5;
    private float jumpcount = 0;
    private float angle = 0;
    private float preEulerAngle = 0;
    [SerializeField]private float inputAngle = 0;
    private Vector3 AxB = Vector3.zero;
    private bool isPress = false;
    private bool isFlick = false;
    private bool flick_up, flick_down, flick_right, flick_left;
    private bool isTouch = false;
    private Platform platform;

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
            controllerObject.SetActive(true);
            controller.transform.rotation = Quaternion.identity;
            mousePosition1 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>()
                .ScreenToViewportPoint(Input.mousePosition);
            //mousePosition1.y = mousePosition1.y - 0.1f;
            var controllerPosition = new Vector3(mousePosition1.x, mousePosition1.y - 0.1f, 0);
            controllerObject.transform.position =
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ViewportToScreenPoint(controllerPosition);
            prepos = controllerObject.transform.position;
            isPress = true;
            isFlick = false;
        }
        if (Input.GetMouseButton(0) && isPress)
        {
            jumpcount += Time.deltaTime;
            mousePosition2 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().
                ScreenToViewportPoint(Input.mousePosition);
            pos = mousePosition1;
            vecA = prepos - pos;
            vecB = mousePosition2 - pos;
            angle = Vector2.Angle(vecA, vecB);
            AxB = Vector3.Cross(vecA, vecB);
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
            Vector2 flickpos = mousePosition5 - pos;
            if (jumpcount <= jumpInterval)
            {
                if (flickpos.x > flickDistance)
                {
                    flick_right = true;
                    isFlick = true;
                }
                else if (flickpos.x < -flickDistance)
                {
                    flick_left = true;
                    isFlick = true;
                }
                if (flickpos.y > flickDistance)
                {
                    flick_up = true;
                    isFlick = true;
                }
                else if (flickpos.y < -flickDistance)
                {
                    flick_down = true;
                    isFlick = true;
                }

            }
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
