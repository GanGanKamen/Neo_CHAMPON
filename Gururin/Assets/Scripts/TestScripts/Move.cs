using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float[] speed;
    public float jumpSpeed;
    public bool[] isRot;
    public bool isPress;
    public bool isMove = true;
    private bool isJump = false;
    private Rigidbody2D rb2d;

    public bool gearHit;

    [SerializeField] FlagManager flagManager;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    /*void OnTriggerStay2D(Collider2D other)
    {
        //歯車と衝突したら動きを止める
        if (other.CompareTag("Gimmick"))
        {
            rb2d.velocity = Vector2.zero;
            //GetComponent<PolygonCollider2D>().enabled = false;
        }
    }*/

    //地面に接触したときなので空中ジャンプができてしまう　要改良
    void OnCollisionEnter2D(Collision2D other)
    {
        //"Ground"と接地時にジャンプを可能にする
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            speed[0] = 5.0f;
            speed[1] = 0.0f;
        }
    }

    void Update()
    {
        if (flagManager.moveStop)
        {
            //位置を固定
            rb2d.velocity = Vector2.zero;
            //角度を固定
            rb2d.angularVelocity = 0.0f;
        }

        if (Input.GetKey(KeyCode.RightArrow) && isMove)
        {
            isRot[0] = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && isMove)
        {
            isRot[1] = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPress = true;
        }
    }

    void FixedUpdate()
    {
        if (isRot[0])
        {
            Vector2 force = new Vector2(speed[0], speed[1]);
            rb2d.AddForce(force);
            isRot[0] = false;
        }
        if (isRot[1])
        {
            if (!flagManager.isMove_VG[1] && !flagManager.isMove_VG[2])
            {
                Vector2 force = new Vector2(speed[0] * -1.0f, speed[1]);
                rb2d.AddForce(force);
            }
            //壁に張り付いているとき
            if (flagManager.isMove_VG[1] || flagManager.isMove_VG[2])
            {
                Vector2 force = new Vector2(speed[0], speed[1] * -1.0f);
                rb2d.AddForce(force);
            }
            isRot[1] = false;
        }
        if (isPress)
        {
            if (!gearHit && !isJump)
            {
                rb2d.AddForce(Vector2.up * jumpSpeed);
                isJump = true;
            }
            if (gearHit)
            {
                //Gearと噛み合っているときにジャンプのみすると後方へジャンプ
                //移動操作を行いながらジャンプすると移動分の距離が加算される = 後方ジャンプになる？
                //左方向のみが後方ならばこのままでよいが、右方向へバックジャンプする場合は要改良
                Vector2 force = new Vector2(-200.0f, 0.0f);
                rb2d.AddForce(force);
                isMove = true;
            }
            isPress = false;
        }
    }
}
