using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class PlayerCtrl : GanGanKamen.GururinBase
    {
        public bool CanCtrl { get { return canCtrl; } }
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
        void Start()
        {
            SetDefult();
        }

        // Update is called once per frame
        void Update()
        {
            if (IsAttachGimmick == false && canCtrl)
            {
                GururinMove();
                NormalCtrl();
            }
            /*
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PermitControll();
            }
            */
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



