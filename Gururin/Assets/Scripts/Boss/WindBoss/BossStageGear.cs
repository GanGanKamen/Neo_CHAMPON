using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class BossStageGear : MonoBehaviour
    {
        [SerializeField]private GameObject gear;
        public float rotSpeed;
        public BossStageFloor moveFloor;
        [Range(-1, 1)] public int direction;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GearTurn(bool obstruction,bool isPlayer)
        {
            if(obstruction == true)
            {
                if(isPlayer == false) gear.transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed * direction));
                moveFloor.MoveFloor(true);
            }
            else
            {
                if (isPlayer == false) gear.transform.Rotate(new Vector3(0.0f, 0.0f, -rotSpeed * direction));
                moveFloor.MoveFloor(false);
            }
        }
    }
}

