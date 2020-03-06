using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class PlayerCtrl : GanGanKamen.GururinBase
    {
        public bool CanCtrl { get { return canCtrl; } }

        [SerializeField] private float _maxSpeed;
        [SerializeField] private GameObject _gear;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _accel;
        [SerializeField] [Range(0, 10)] private float _brakePower;

        public GameController gameController;

        private bool canCtrl = false;


        public void PermitControll()
        {
            canCtrl = true;
            gameController.Enable = true;
        }

        public void ProhibitControll()
        {
            canCtrl = false;
            gameController.Enable = false;
        }

        // Start is called before the first frame update
        private void Awake()
        {
            maxSpeed = _maxSpeed;
            gear = _gear;
            jumpPower = _jumpPower;
            accel = _accel;
            brakePower = _brakePower;

            SetDefult();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (IsAttachGimmick == false && canCtrl)
            {
                GururinMove();
                NormalCtrl();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PermitControll();
            }
        }

        private void NormalCtrl()
        {
            //Debug.Log(gameController.InputAngle);
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
}



