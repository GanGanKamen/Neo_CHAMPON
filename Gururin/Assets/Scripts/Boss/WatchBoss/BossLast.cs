using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WatchBoss
{
    public class BossLast : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private Transform muzzle;
        [SerializeField] private float speed;
        [SerializeField] private float maxHeight;
        [SerializeField] private SpriteRenderer yarn;
        private Vector3 startPos;
        private Vector3 attackPos;

        private float moveCount = Mathf.PI;
        [SerializeField] private bool direction = false;
        private bool changeDirection = false;
        // Start is called before the first frame update
        void Start()
        {
            startPos = startPoint.position;
            attackPos = attackPoint.position;

            transform.position = attackPos;

        }

        // Update is called once per frame
        void Update()
        {
            if (direction == false)
            {
                moveCount += Time.deltaTime * speed;
                if(transform.position.y >= maxHeight && changeDirection == false)
                {
                    direction = true;
                    changeDirection = true;
                }
            }
            else
            {
                moveCount -= Time.deltaTime * speed;
                if (transform.position.y >= maxHeight && changeDirection == false)
                {
                    direction = false;
                    changeDirection = true;
                }
            }

            if (changeDirection)
            {
                if (transform.position.y < maxHeight)
                {
                    changeDirection = false;
                }
            }
            RadiusMove();
        }

        private void RadiusMove()
        {
            var radius = Vector3.Distance(attackPos, startPos);
            var x = Mathf.Sin(moveCount) * radius + attackPos.x;
            var y = Mathf.Cos(moveCount) * radius + attackPos.y + radius;
            transform.position = new Vector3(x, y, 0);
            yarn.transform.position = new Vector3((transform.position.x + startPos.x) / 2,
                (transform.position.y + startPos.y) / 2, 0);
            yarn.size = new Vector2(0.2f, radius);
            var vec = (yarn.transform.position - startPos).normalized;
            yarn.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        }
    }
}


