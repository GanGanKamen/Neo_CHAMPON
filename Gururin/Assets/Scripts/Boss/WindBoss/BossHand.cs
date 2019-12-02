using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class BossHand : MonoBehaviour
    {
        private PlayerMove player;
        private Gamecontroller gameController;
        private GameObject handParent;


        private Vector3 startPos;

        [SerializeField] private Vector2 moveRangeX;
        [SerializeField] private Vector2 moveRangeY;
        [SerializeField] private GearGimmick thisGear;
        //[SerializeField] private BossStageGear floorGimickGear;
        [SerializeField] private Vector3 GearPosOffest;
        [SerializeField] private BossBalloon bossBalloon;
        [Range(-1, 1)] [SerializeField] int direction;
        private Vector2 distinationPos;
        private bool hitPlayer = false;

        public enum Pattern
        {
            RandomWalk,
            Stop,
            Stay,
            Kill,
            ReadyToKill,
            Attack,
            Recovery
        }
        public Pattern pattern;

        public enum Hand
        {
            Left,
            Right
        }
        public Hand hand;

        private float moveSpeed;
        [SerializeField] private Transform deathZones;
        [SerializeField] private float attackProbability;
        private float attackCount;
        private CapsuleCollider2D capsuleCollider;
        private PolygonCollider2D gearColider;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
            gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            gearColider = thisGear.gear.GetComponent<PolygonCollider2D>();
            handParent = transform.parent.gameObject;
            distinationPos = new Vector2(Random.Range(moveRangeX.x, moveRangeX.y),
                Random.Range(moveRangeY.x, moveRangeY.y));
            pattern = Pattern.RandomWalk;
            startPos = handParent.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            var speedPlus = 4 - bossBalloon.lifes;
            handParent.transform.position = Vector2.Lerp(handParent.transform.position, distinationPos, moveSpeed);
            switch (pattern)
            {
                case Pattern.RandomWalk:
                    attackCount = 0;
                    RandomWalk();
                    ColliderCancel();
                    break;
                case Pattern.Stay:
                    distinationPos = transform.position;
                    attackCount += Time.deltaTime;
                    moveSpeed = 0;
                    if(attackCount >= 0.2f)
                    {
                        pattern = Pattern.ReadyToKill;
                    }
                    break;
                case Pattern.ReadyToKill:
                    distinationPos = startPos;
                    attackCount = 0;
                    moveSpeed = Time.deltaTime * 2f;
                    if (Vector3.Distance(handParent.transform.position, distinationPos) < 0.5f)
                    {
                        pattern = Pattern.Kill;
                        SoundManager.PlayS(handParent, "SE_propellerBOSSnakigoe1");
                    }
                    ColliderCancel();
                    break;
                case Pattern.Kill:
                    Vector3 offest = Vector3.Scale((player.transform.position - transform.position), new Vector3(-1, -1, 0)).normalized;
                    distinationPos = deathZones.position + offest;
                    attackCount = 0;
                    moveSpeed = Time.deltaTime * 0.5f * speedPlus;
                    if (Vector3.Distance(handParent.transform.position, distinationPos) < 0.2f)
                    {
                        pattern = Pattern.RandomWalk;
                        distinationPos = new Vector2(Random.Range(moveRangeX.x, moveRangeX.y),
                Random.Range(moveRangeY.x, moveRangeY.y));
                    }
                    ColliderCancel();
                    break;
                case Pattern.Attack:
                    attackCount += Time.deltaTime;
                    moveSpeed = Time.deltaTime * 0.5f * speedPlus;
                    if (player.transform.position.x < moveRangeX.x)
                    {
                        distinationPos = new Vector2(moveRangeX.x+1, player.transform.position.y);
                    }
                    else if (player.transform.position.x > moveRangeX.y)
                    {
                        distinationPos = new Vector2(moveRangeX.y-1, player.transform.position.y);
                    }
                    else
                    {
                        distinationPos = player.transform.position;
                    }

                    if (attackCount >= 5)
                    {
                        attackCount = 0;
                        pattern = Pattern.RandomWalk;
                        distinationPos = new Vector2(Random.Range(moveRangeX.x, moveRangeX.y),
        Random.Range(moveRangeY.x, moveRangeY.y));
                    }
                    
                        ColliderCancel();
                    break;

                case Pattern.Recovery:
                    distinationPos = startPos;
                    capsuleCollider.enabled = false;
                    gearColider.enabled = false;
                    if (Vector3.Distance(handParent.transform.position, distinationPos) < 0.2f && player.isJump == true
                        && bossBalloon.status != BossBalloon.Status.Hit)
                    {
                        pattern = Pattern.RandomWalk;
                    }
                    moveSpeed = Time.deltaTime * 2;
                    break;
                case Pattern.Stop:
                    distinationPos = transform.position;
                    capsuleCollider.enabled = false;
                    gearColider.enabled = false;
                    break;
            }

            /*
            if (gameController.isPress && hitPlayer)
            {
                if (gameController.AxB.z < 0)
                {
                    handParent.transform.Rotate(0, 0, thisGear.rotSpeed);
                }
                else if (gameController.AxB.z > 0)
                {
                    handParent.transform.Rotate(0, 0, -thisGear.rotSpeed);
                }

            }*/
            if (hitPlayer == false)
            {
                thisGear.gear.transform.Rotate(new Vector3(0.0f, 0.0f, thisGear.rotSpeed * direction));
            }

        }
        private void ColliderCancel()
        {
            if (player.nowBossHand != null && player.nowBossHand != this)
            {
                capsuleCollider.enabled = false;
            }
            else
            {
                capsuleCollider.enabled = true;
            }
            gearColider.enabled = true;
        }

        private void RandomWalk()
        {
            moveSpeed = Time.deltaTime / 2f;
            if (Vector3.Distance(handParent.transform.position, distinationPos) < 0.5f)
            {
                distinationPos = new Vector2(Random.Range(moveRangeX.x, moveRangeX.y),
                Random.Range(moveRangeY.x, moveRangeY.y));
            }
        }

        public void AttachPlayer()
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.transform.parent = this.transform;
            player.nowBossHand = this;
            hitPlayer = true;
            attackCount = 0;
            pattern = Pattern.Stay;

        }

        public void Separate()
        {
            player.transform.parent = null;
            hitPlayer = false;
            pattern = Pattern.Recovery;
            distinationPos = startPos;
        }
    }
}


