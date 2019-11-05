using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class BossBalloon : MonoBehaviour
    {
        public Sprite[] eyes;
        [SerializeField] private SpriteRenderer bosseye;
        private PlayerMove player;
        public BossHand[] hands; //0:lefthand 1:righthand
        public int lifes;
        private int fullLifes;

        //private SpriteRenderer sprite;
        private Animator balloonAnim;
        private AnimatorStateInfo balloonAnimInfo;
        //private float recovery = 0;

        [SerializeField] private float attackProbability;
        [SerializeField]private float attackCount;

        public enum Status
        {
            StandBy,
            Action,
            Hit
        }
        public Status status;

        public Animator bossAnim;

        public bool isDead = false;

        public GameObject bossCadaver;

        public Sprite[] lifeImages;

        public SpriteRenderer lifesprite;

        private bool isDown = false;
        // Start is called before the first frame update
        void Start()
        {
            balloonAnim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
            bosseye.sprite = eyes[0];
        }

        // Update is called once per frame
        void Update()
        {
            balloonAnimInfo = balloonAnim.GetCurrentAnimatorStateInfo(0);
            //DamageHit();

            StatusManager();

            lifesprite.sprite = lifeImages[lifes];
        }

        private void StatusManager()
        {
            if (hands[0].pattern == BossHand.Pattern.RandomWalk && hands[1].pattern == BossHand.Pattern.RandomWalk)
            {
                status = Status.StandBy;
            }
            else if (status != Status.Hit)
            {
                status = Status.Action;
                attackCount = 0;
            }
            if (status == Status.StandBy)
            {
                attackCount += Time.deltaTime;
                if (attackCount >= attackProbability)
                {
                    attackCount = 0;
                    for (int i = 0; i < hands.Length; i++)
                    {
                        bool isAttack = HandCheck(hands[i].hand);
                        if (isAttack == true)
                        {
                            hands[i].pattern = BossHand.Pattern.Attack;
                        }
                        else
                        {
                            hands[i].pattern = BossHand.Pattern.RandomWalk;
                        }
                    }
                    status = Status.Action;
                    SoundManager.PlayS(gameObject, "SE_propellerBOSSnakigoe1");
                }
            }
        }

        /*
        private void DamageHit()
        {
            if (status == Status.Hit && isDead == false)
            {
                sprite.color = Color.clear;
                lifesprite.color = Color.clear;
                recovery += Time.deltaTime;
                for (int i = 0; i < hands.Length; i++)
                {
                    hands[i].pattern = BossHand.Pattern.Stop;
                }
                if (recovery > 4f)
                {
                    recovery = 0;
                    bosseye.sprite = eyes[1];
                    SoundManager.PlayS(gameObject, "SE_propellerBOSSnakigoe1");
                    for (int i = 0; i < hands.Length; i++)
                    {
                        hands[i].pattern = BossHand.Pattern.RandomWalk;
                    }
                }
            }
            else
            {
                sprite.color = Color.white;
                lifesprite.color = Color.white;
            }
        }
        */

        public bool HandCheck(BossHand.Hand hand)
        {
            switch (hand)
            {
                case BossHand.Hand.Left:
                    if (player.transform.position.x <= transform.position.x)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case BossHand.Hand.Right:
                    if (player.transform.position.x >= transform.position.x)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (isDown) return;
            if (collision.CompareTag("Player"))
            {
                PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
                if (player.nowBossHand != null)
                {
                    return;
                }
                status = Status.Hit;
                Debug.Log("Stay");
                bosseye.sprite = eyes[2];
                lifes--;
                SoundManager.PlayS(gameObject, "SE_ballonBreak");
                if (lifes == 0)
                {
                    StartCoroutine(Dead());
                }

                else
                {
                    StartCoroutine(Down());
                }

            }
        }
        
        private IEnumerator Down()
        {
            if (isDown) yield break;
            isDown = true;
            Debug.Log("down");
            lifesprite.color = Color.clear;
            for (int i = 0; i < hands.Length; i++)
            {
                hands[i].pattern = BossHand.Pattern.Stop;
            }
            bossAnim.SetTrigger("Down");
            balloonAnim.SetBool("Break",true);
            SoundManager.PlayS(gameObject, "SE_propellerBOSSnakigoe2");
            yield return new WaitForSeconds(3f);
            balloonAnim.SetBool("Break", false);
            bosseye.sprite = eyes[1];
            SoundManager.PlayS(gameObject, "SE_propellerBOSSnakigoe1");
            for (int i = 0; i < hands.Length; i++)
            {
                hands[i].pattern = BossHand.Pattern.RandomWalk;
            }
            lifesprite.color = Color.white;
            isDown = false;
            yield break;
        }

        private IEnumerator Dead()
        {
            if (isDown) yield break;
            isDown = true;
            player.transform.parent = null;
            isDead = true;
            balloonAnim.SetBool("Break", true);
            balloonAnim.SetBool("Death", true);
            lifesprite.enabled = false;
            bossAnim.SetTrigger("Dead");
            for(int i = 0; i < hands.Length; i++)
            {
                hands[i].pattern = BossHand.Pattern.Stop;
            }
            yield return new WaitForSeconds(4f);
            /*
            yield return new WaitForSeconds(2f);
            SoundManager.PlayS(gameObject, "SE_glassBreak");
            yield return new WaitForSeconds(2f);*/
            bossCadaver.SetActive(true);
            transform.parent.gameObject.SetActive(false);
            yield break;

        }
    }
}

