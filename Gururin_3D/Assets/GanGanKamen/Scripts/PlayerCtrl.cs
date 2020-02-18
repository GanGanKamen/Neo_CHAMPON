using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : GanGanKamen.GururinBase
{
    public bool CanCtrl { get { return canCtrl; } }

    [SerializeField] public float _maxSpeed;
    [SerializeField] private GameObject _gear;
    [SerializeField] private float _jumpPower;
    [SerializeField] public float _accel;
    [SerializeField] [Range(0, 10)] private float _brakePower;

    [SerializeField] private GanGanKamen.GameController gameController;

    private bool canCtrl = false;
    // Start is called before the first frame update
    private void Awake()
    {
        maxSpeed = _maxSpeed;
        gear = _gear;
        jumpPower = _jumpPower;
        accel = _accel;
        brakePower = _brakePower;
        canCtrl = true;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GururinMove();
        TouchCtrl();
    }

    private void TouchCtrl()
    {
        if (gameController.InputAngle != 0)
        {
            SetAngle(gameController.InputAngle);
        }
        else
        {
            if (IsCollideWall) StandStill();
        }
        if (gameController.InputFlick)
        {
            Jump();
        }
        else if (gameController.InputLongPress)
        {
            Brake();
        }
    }
}

