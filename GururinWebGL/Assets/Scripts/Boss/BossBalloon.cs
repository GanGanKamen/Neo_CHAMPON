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

        private SpriteRenderer sprite;
        private float recovery = 0;

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
        // Start is called before the first frame update
        void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
            bosseye.sprite = eyes[0];
        }

        // Update is called once per frame
        void Update()
        {

            DamageHit();

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
            if (status == Status.Hit) return;
            if (collision.CompareTag("Player"))
            {
                PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
                if (player.nowBossHand != null)
                {
                    return;
                }
                status = Status.Hit;
                bosseye.sprite = eyes[2];
                lifes--;
                SoundManager.PlayS(gameObject, "SE_ballonBreak");
                if (lifes == 0)
                {
                    StartCoroutine(Dead());
                }

                else
                {
                    bossAnim.SetTrigger("Down");
                }

            }
        }
        /*
        private IEnumerator Down()
        {
            bossAnim.SetTrigger("Down");
            SoundManager.PlayS(gameObject, "SE_propellerBOSSnakigoe2");
            yield return new WaitForSeconds(0.4f);
            SoundManager.PlayS(gameObject, "SE_glassCrack");
            yield break;
        }*/

        private IEnumerator Dead()
        {
            player.transform.parent = null;
            isDead = true;
            sprite.enabled = false;
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

