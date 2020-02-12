using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : GururinBase
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private GameObject _gear;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _accel;
    [SerializeField][Range(0,10)] private float _brakePower;

    [SerializeField] private GameController gameController;
    // Start is called before the first frame update
    private void Awake()
    {
        maxSpeed = _maxSpeed;
        gear = _gear;
        jumpPower = _jumpPower;
        accel = _accel;
        brakePower = _brakePower;
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
