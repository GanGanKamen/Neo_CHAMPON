using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class DeadZone : MonoBehaviour
    {
        public BossBalloon boss;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void BossSceneReload()
        {
            RemainingLife.bossLife = boss.lifes;
        }
    }
}

