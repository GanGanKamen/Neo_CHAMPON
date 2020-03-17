using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private GanGanKamen.GururinBase gururinBase;
    [SerializeField] private GameObject player;
    //move 動き 
    [SerializeField] private int move, moveMode, interval;
    [SerializeField] private float speed, distance;

    private int count, countMax, intervalcount;
    private bool firsttime, isInterval;
    private Vector3 firstpos;


    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        firsttime = true;
        firstpos = this.gameObject.transform.position;
        if(interval > 0)
        {
            isInterval = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Liftmove();
    }

    private void Liftmove()
    {
        switch (move)
        {
            case 0://上下移動 上→下
                moveMode = 0;
                //初回動作
                if (this.gameObject.transform.position.y - firstpos.y < distance && firsttime)
                {
                    this.gameObject.transform.Translate(0, speed, 0);
                }
                else if(this.gameObject.transform.position.y - firstpos.y >= distance && firsttime)
                {
                    countMax = count * 2;
                    this.gameObject.transform.Translate(0, -speed, 0);
                    firsttime = false;
                }
                //往復動作以降
                else if(count >= 0 && count < countMax / 2 && !firsttime)
                {
                    this.gameObject.transform.Translate(0, speed, 0);
                    if (interval > 0 && !isInterval)
                    {
                        isInterval = true;
                    }
                }
                else if(count == countMax /2 && !firsttime)
                {
                    /*if(isInterval)//インターバルでの停止動作1
                    {
                        if(intervalcount < interval)
                        {
                            intervalcount++;
                            count--;
                        }
                        else if (intervalcount == interval)
                        {
                            intervalcount = 0;
                            isInterval = false;
                        }
                    }
                    else
                    {
                        this.gameObject.transform.Translate(0, -speed, 0);
                    }*/
                    this.gameObject.transform.Translate(0, -speed, 0);
                }
                else if(count > countMax / 2 && count < countMax && !firsttime)
                {
                    this.gameObject.transform.Translate(0, -speed, 0);
                    if(interval > 0 && !isInterval)
                    {
                        isInterval = true;
                    }
                }
                else if(count == countMax && !firsttime)
                {
                    /*if(isInterval)//インターバルでの停止動作2
                    {
                        if (intervalcount < interval)
                        {
                            intervalcount++;
                            count--;
                        }
                        else if (intervalcount == interval)
                        {
                            intervalcount = 0;
                            isInterval = false;
                        }
                    }
                    else
                    {
                        this.gameObject.transform.Translate(0, speed, 0);
                        count = 0;
                    }*/
                    this.gameObject.transform.Translate(0, speed, 0);
                    count = 0;
                }
                break;

            case 1://左右移動
                //初回動作
                if (this.gameObject.transform.position.x - firstpos.x < distance && firsttime)
                {
                    moveMode = 1;
                    this.gameObject.transform.Translate(speed, 0, 0);
                }
                else if (this.gameObject.transform.position.x - firstpos.x >= distance && firsttime)
                {
                    countMax = count * 2;
                    moveMode = 2;
                    this.gameObject.transform.Translate(-speed, 0, 0);
                    firsttime = false;
                }

                //往復動作以降
                else if (count >= 0 && count < countMax / 2 && !firsttime)
                {
                    moveMode = 1;
                    this.gameObject.transform.Translate(speed, 0, 0);
                    if (interval > 0 && !isInterval)
                    {
                        isInterval = true;
                    }
                }
                else if (count == countMax / 2 && !firsttime)
                {
                    /*if(isInterval)//インターバルでの停止動作1
                    {
                        if(intervalcount < interval)
                        {
                            intervalcount++;
                            count--;
                        }
                        else if (intervalcount == interval)
                        {
                            intervalcount = 0;
                            isInterval = false;
                        }
                    }
                    else
                    {
                        this.gameObject.transform.Translate(0, -speed, 0);
                    }*/
                    moveMode = 2;
                    this.gameObject.transform.Translate(-speed, 0, 0);
                }
                else if (count > countMax / 2 && count < countMax && !firsttime)
                {
                    moveMode = 2;
                    this.gameObject.transform.Translate(-speed, 0, 0);
                    if (interval > 0 && !isInterval)
                    {
                        isInterval = true;
                    }
                }
                else if (count == countMax && !firsttime)
                {
                    /*if(isInterval)//インターバルでの停止動作2
                    {
                        if (intervalcount < interval)
                        {
                            intervalcount++;
                            count--;
                        }
                        else if (intervalcount == interval)
                        {
                            intervalcount = 0;
                            isInterval = false;
                        }
                    }
                    else
                    {
                        this.gameObject.transform.Translate(0, speed, 0);
                        count = 0;
                    }*/
                    moveMode = 1;
                    this.gameObject.transform.Translate(speed, 0, 0);
                    count = 0;
                }
                break;
        }
        count++;
    }

    private void OnTriggerStay(Collider other)
    {

        var playerRb = player.GetComponent<Rigidbody>();
        if (other.CompareTag("Player"))
        {
            gururinBase.isLift = true;
            if(moveMode == 0)
            {
                //gururinBase.liftSpeed = 0;
                gururinBase.liftpos = new Vector3(0f, 0f, 0f);
            }
            else if(moveMode == 1)
            {
                //gururinBase.liftSpeed = speed;
                gururinBase.liftpos = new Vector3(speed, 0f, 0f);
            }
            else if(moveMode == 2)
            {
                //gururinBase.liftSpeed = -speed;
                gururinBase.liftpos = new Vector3(-speed, 0, 0);
            }

            if(moveMode == 1 && Input.GetMouseButton(0) == false)
            {
                playerRb.AddForce(50f * speed, 0, 0);
            }
            else if(moveMode == 2 && Input.GetMouseButton(0) == false)
            {
                playerRb.AddForce(-50f * speed, 0, 0);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gururinBase.isLift = false;
        }
    }
}
