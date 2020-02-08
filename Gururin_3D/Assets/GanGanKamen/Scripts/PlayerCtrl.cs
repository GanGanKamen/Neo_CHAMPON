using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : GururinBase
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _gear;
    [SerializeField] private float _jumpPower;

    // Start is called before the first frame update
    private void Awake()
    {
        speed = _speed;
        gear = _gear;
        jumpPower = _jumpPower;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TestCtrl();
    }

    private void TestCtrl()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                base.GururinMove(Input.GetAxis("Horizontal") * 360, true);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                base.GururinMove(-Input.GetAxis("Horizontal") * 360, false);
            }
        }
        else
        {
            base.SpeedReset();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            base.Jump();
        }
    }


}
