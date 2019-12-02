using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class BossStageFloor : MonoBehaviour
    {
        public bool isLimit;
        private Vector3 startPos;
        private Vector3 limitPos;
        [SerializeField] private float moveLimit;
        [SerializeField] private float speed;
        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.position;
            limitPos = new Vector3(moveLimit, transform.position.y, transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            if (Mathf.Abs(transform.position.x - moveLimit) < 0.5f) isLimit = true;
            else isLimit = false;
        }

        public void MoveFloor(bool obstruction)
        {
            switch (obstruction)
            {
                case true:
                    transform.position = Vector3.Lerp(transform.position, limitPos, Time.deltaTime * speed);
                    break;
                case false:
                    transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * speed);
                    break;
            }
        }
    }

}
