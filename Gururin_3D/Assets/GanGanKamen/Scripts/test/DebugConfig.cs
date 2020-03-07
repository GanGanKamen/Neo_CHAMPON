using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GanGanKamen
{
    public class DebugConfig : MonoBehaviour
    {
        [SerializeField] Text debugText;
        private PlayerCtrl player;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetSceneChange();
            TextUpdate();
        }

        private void TextUpdate()
        {
            if (player == null) debugText.text = "null";
            else
            {
                debugText.text = "IsAttachGimmick: " + player.IsAttachGimmick
                    + "\n" + "CanJump: " + player.CanJump
                    + "\n" + "CanCtrl: " + player.CanCtrl
                    + "\n" + "IsAccelMove: " + player.IsAccelMove
                    + "\n" + "debugRotSpeed: " + player.debugRotSpeed
                    + "\n" + "debugMoveVecSpeed" + player.debugMoveVecSpeed
                    + "\n" + "velocity: " + player.GetComponent<Rigidbody>().velocity
                    + "\n" + "MoveAngle: " + player.MoveAngle
                    + "\n" + "maxSpeed" + player.maxSpeed
                    + "\n" + "accel" + player.accel
                     + "\n" + "defultSpeed" + player.defultSpeed;
            }
        }

        private void GetSceneChange()
        {
            if (GameStart.isSceneChange)
            {
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                }
                else
                {
                    player = null;
                }
            }
        }
    }
}

