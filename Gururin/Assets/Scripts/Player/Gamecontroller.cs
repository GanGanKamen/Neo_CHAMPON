﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gamecontroller : MonoBehaviour
{
    public PlayerMove playerMove; //Moveスクリプトの呼び出し
    public GameObject controllerObject;
    public GameObject controller/*, Arrow_RR, Arrow_RB, Arrow_LR, Arrow_LB*/; //コントローラーオブジェクト
    public GameObject camera; //カメラオブジェクト
    public Vector2 mousePosition1, poslimit, prepos, pos, vecA, vecB; //マウスの初期位置 フリック判定範囲制限 1フレーム前のマウス位置 現在のコントローラー位置 マウス位置の2ベクトル
    public Vector2 mousePosition2, mousePosition3, mousePosition4, mousePosition5;
    public Vector2 initialpos1, initialpos2, initialpos3; //jampcountの初期化のための位置取得
    public float jumpcount, jump; //フレーム毎のカウント　ジャンプ出来るまでのカウント数
    public int initialcount, initial; //jampcount初期化のフレーム毎のカウント　初期化までのカウント数
    public int timercount; //押してから離れるまでのフレーム毎のカウント
    public float angle, flick, area; //角度　フリックできる距離の値　フリック初期化の範囲の値
    public Vector3 AxB; //外積
    public bool isPress, isFlick; //タップされているか、フリックされているかどうか　これがないと一度タップして以降常時タップ判定が出たままになる
    public bool flick_up, flick_down, flick_right, flick_left; //上下左右のフリック判定
    public bool isArrowR, isArrowL;
    public int arrowcount;

    public bool isCon, isDes;
    public bool touch;

    public Configuration config;
    public float sensitivity;
    public int controllerfixed, controllerposition;

    [SerializeField] FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();

        //初期設定でコントローラーの非活性化
        controllerObject.SetActive(false);
        /*Arrow_RR.SetActive(false);
        Arrow_RB.SetActive(false);
        Arrow_LR.SetActive(false);
        Arrow_LB.SetActive(false);*/
        poslimit.x = 0.12f;
        poslimit.y = 0.15f;
        jump = 0.5f;
        initial = 10;
        area = 0.2f;
        isFlick = false;
        touch = false;

        sensitivity = config.sensitivity;
        controllerfixed = config.controllerfixed;
        controllerposition = config.controllerposition;

        flick = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (config.configbutton || isCon)
        {
            flagManager.pressParm = false;
        }
        else if (!config.configbutton && !isCon)
        {
            flagManager.pressParm = true;
        }

        sensitivity = config.sensitivity;
        controllerfixed = config.controllerfixed;
        controllerposition = config.controllerposition;


        if (flagManager.pressParm)
        {
            if (Input.touchCount > 0)
            {
                touch = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if(controllerfixed == 0)
                {
                    //jumpcountの初期化
                    jumpcount = 0;
                }
                //initialcountの初期化
                initialcount = 0;
                //timercountの初期化
                timercount = 0;
                //コントローラーの活性化
                controllerObject.SetActive(true);
                controller.transform.rotation = Quaternion.identity;

                //コントローラーの位置取得
                if (touch)
                {
                    mousePosition1 = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                }
                else if (!touch)
                {
                    mousePosition1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                }
                /*if (mousePosition1.x > 0.94f && mousePosition1.y > 0.91f ||
                    mousePosition1.x < 0.19f && mousePosition1.y < 0.09f)
                {
                    controllerObject.SetActive(false);
                }
                else
                {
                }*/
                switch (controllerfixed)
                {
                    case 0:
                        if (controllerposition == 0)
                        {
                            mousePosition1.y = mousePosition1.y - 0.1f;
                        }
                        else if (controllerposition == 2)
                        {
                            mousePosition1.y = mousePosition1.y + 0.1f;
                            controller.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180f);
                        }

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
                        break;
                    case 1:
                        if (mousePosition1.x <= 0.5f)
                        {
                            mousePosition1.x = 0.2f;
                        }
                        else
                        {
                            mousePosition1.x = 0.8f;
                        }
                        mousePosition1.y = 0.2f;
                        break;
                    default:
                        break;
                }
                
                controllerObject.transform.position = Camera.main.ViewportToScreenPoint(mousePosition1);

                //preposにコントローラーの位置を代入　初期設定
                prepos = controllerObject.transform.position;


                isPress = true;
                isFlick = false;
            }

            if (Input.GetMouseButton(0) && isPress)
            {
                if(controllerfixed == 0)
                {
                    //フレーム毎にjumpcountをカウントする
                    jumpcount += Time.deltaTime;
                }
                //フレーム毎にtimercountをカウントする 
                timercount++;

                //現在のマウス位置を取得しmousePosition2に代入
                if (touch)
                {
                    mousePosition2 = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                }
                else if (!touch)
                {
                    mousePosition2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                }

                //コントローラーの位置をposに代入しpreposと現在のマウス位置のそれぞれからベクトルを求める
                pos = mousePosition1;
                vecA = prepos - pos;
                vecB = mousePosition2 - pos;

                //timercount10以上の時、2つのベクトルから角度を計算し、外積を求める
                if (timercount >= 10)
                {
                    angle = Vector2.Angle(vecA, vecB);
                    AxB = Vector3.Cross(vecA, vecB);
                }

                //外積が正の時の処理
                if (AxB.z > 0)
                {
                    controller.transform.rotation = controller.transform.rotation * Quaternion.Euler(0, 0, angle);
                }
                //外積が負の時の処理
                else if (AxB.z < 0)
                {
                    controller.transform.rotation = controller.transform.rotation * Quaternion.Euler(0, 0, -angle);
                }

                //preposにこの時点でのマウス位置を代入 camerapos2にこの時点でのカメラ位置を再代入
                prepos = mousePosition2;
            }

            if(controllerfixed == 1 && Input.touchCount > 1)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    Debug.Log("touch 1.1 :" + Input.GetTouch(1).position);
                    jumpcount = 0;
                    mousePosition3 = Camera.main.ScreenToViewportPoint(Input.GetTouch(1).position);
                }
                if (Input.GetTouch(1).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Stationary)
                {
                    Debug.Log("touch 1.2 :" + Input.GetTouch(1).position);
                    jumpcount += Time.deltaTime;
                }
                if (Input.GetTouch(1).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Canceled)
                {
                    Debug.Log("touch 1.3 :" + Input.GetTouch(1).position);
                    mousePosition4 = Camera.main.ScreenToViewportPoint(Input.GetTouch(1).position);
                    Vector2 flickpos = mousePosition4 - mousePosition3;

                    if (jumpcount <= jump)
                    {
                        if (flickpos.x > flick)
                        {
                            flick_right = true;
                            isFlick = true;
                        }
                        else if (flickpos.x < -flick)
                        {
                            flick_left = true;
                            isFlick = true;
                        }
                        if (flickpos.y > flick)
                        {
                            flick_up = true;
                            isFlick = true;
                        }
                        else if (flickpos.y < -flick)
                        {
                            flick_down = true;
                            isFlick = true;
                        }
                    }
                }
                playerMove.isPress = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if(controllerfixed == 0)
                {
                    if (!NeoConfig.isToutchToJump)
                    {
                        if (touch)
                        {
                            mousePosition5 = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                        }
                        else if (!touch)
                        {
                            mousePosition5 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                        }

                        Vector2 flickpos = mousePosition5 - pos;


                        if (jumpcount <= jump)
                        {
                            if (flickpos.x > flick)
                            {
                                flick_right = true;
                                isFlick = true;
                            }
                            else if (flickpos.x < -flick)
                            {
                                flick_left = true;
                                isFlick = true;
                            }
                            if (flickpos.y > flick)
                            {
                                flick_up = true;
                                isFlick = true;
                            }
                            else if (flickpos.y < -flick)
                            {
                                flick_down = true;
                                isFlick = true;
                            }

                        }

                        playerMove.isPress = true;
                    }

                    else
                    {
                        if (touch)
                        {
                            Vector2 mousePosition5 = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                        }
                        else if (!touch)
                        {
                            Vector2 mousePosition5 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                        }

                        var playerPos = new Vector2(playerMove.transform.position.x, playerMove.transform.position.y);
                        Debug.Log(mousePosition5.y);
                        Debug.Log(playerPos.y);
                        if (jumpcount <= jump)
                        {
                            if (mousePosition5.y >= playerPos.y + 0.5f)
                            {
                                flick_up = true;
                                isFlick = true;
                            }
                            else
                            {
                                if (mousePosition5.x > playerPos.x)
                                {
                                    flick_right = true;
                                    isFlick = true;
                                }
                                else
                                {
                                    flick_left = true;
                                    isFlick = true;
                                }
                            }
                        }
                        playerMove.isPress = true;
                    }
                }
                
                /*
                if (playerMove.nowBossHand!=null)
                {
                    if (playerMove.isJump && (flick_up || flick_down || flick_right || flick_left))
                    {
                        playerMove.isPress = true;
                    }
                }
                else
                {
                    if (playerMove.isJump && flick_up)//ジャンプ
                    {
                        playerMove.isPress = true;
                    }
                }
                */

                //angleの値を初期化
                angle = 0;
                //コントローラーの非活性化
                controllerObject.SetActive(false);
                isPress = false;
                jumpcount = 0;
            }
        }
        else
        {
            controllerObject.SetActive(false);
        }

        //フリックの処理
        if (!isFlick)
        {
            flick_up = false;
            flick_down = false;
            flick_right = false;
            flick_left = false;
        }

        /*if (isArrowR)
        {
            if (AxB.z < -0.1)
            {
                arrowcount = 0;
                Arrow_RB.SetActive(true);
                Arrow_RR.SetActive(false);
            }
            else
            {
                arrowcount++;
                if (arrowcount >= 100)
                {
                    Arrow_RR.SetActive(true);
                    Arrow_RB.SetActive(false);
                }
            }
        }
        else
        {
            Arrow_RB.SetActive(false);
            Arrow_RR.SetActive(false);
        }
        if (isArrowL)
        {
            if (AxB.z > 0.1)
            {
                arrowcount = 0;
                Arrow_LB.SetActive(true);
                Arrow_LR.SetActive(false);
            }
            else
            {
                arrowcount++;
                if (arrowcount >= 100)
                {
                    Arrow_LR.SetActive(true);
                    Arrow_LB.SetActive(false);
                }
            }
        }
        else
        {
            Arrow_LB.SetActive(false);
            Arrow_LR.SetActive(false);
        }*/
    }
}