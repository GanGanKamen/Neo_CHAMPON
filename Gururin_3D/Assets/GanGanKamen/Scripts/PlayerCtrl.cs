using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : GururinBase
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _gear;
    [SerializeField] private float _jumpPower;

    [SerializeField] private GameController gameController;
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
        GururinMove();
        if(gameController.InputAngle != 0)
        {
            SetAngle(gameController.InputAngle);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveStop();
        }
    }
}
