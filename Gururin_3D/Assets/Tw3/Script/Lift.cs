using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private GanGanKamen.GururinBase gururinBase;
    [SerializeField] private GameObject player;
    //move 動き 
    [SerializeField] private int move, moveMode, count, countMax;
    [SerializeField] private float speed;




    // Start is called before the first frame update
    void Start()
    {
        count = 0;
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
            case 0://上下移動
                moveMode = 0;
                if (count >= 0 && count < countMax/4)
                {
                    this.gameObject.transform.Translate(0, speed, 0);
                }
                else if(count >= countMax/4 && count < countMax * 3/4)
                {
                    this.gameObject.transform.Translate(0, -speed, 0);
                }
                else if(count >= countMax * 3/4 && count < countMax)
                {
                    this.gameObject.transform.Translate(0, speed, 0);
                }
                else if(count == countMax)
                {
                    this.gameObject.transform.Translate(0, speed, 0);
                    count = 0;
                }
                break;

            case 1://左右移動
                if (count >= 0 && count < countMax/4)
                {
                    moveMode = 1;
                    this.gameObject.transform.Translate(speed, 0, 0);
                }
                else if (count >= countMax/4 && count < countMax * 3/4)
                {
                    moveMode = 2;
                    this.gameObject.transform.Translate(-speed, 0, 0);
                }
                else if (count >= countMax * 3/4 && count < countMax)
                {
                    moveMode = 1;
                    this.gameObject.transform.Translate(speed, 0, 0);
                }
                else if (count == countMax)
                {
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
