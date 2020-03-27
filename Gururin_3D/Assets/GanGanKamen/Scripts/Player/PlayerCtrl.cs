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

        private PlayerFace _playerFace;
        private GameController gameController;

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
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            SetDefult();
        }

        void Start()
        {
            _playerFace = GetComponentInChildren<PlayerFace>();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log("MaxSpeed  " + maxSpeed);
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
                _playerFace.Angry();
                Brake();
            }
            else
            {
                _playerFace.Nomal();
            }
        }
    }
}



