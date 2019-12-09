using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ぐるりんの動き全般
/// </summary>

public class PlayerMove : MonoBehaviour
{

    public float[] speed; //移動速度
    public bool setSpeed; //基本速度
    public float jumpSpeed; //ジャンプ速度(高さ)
    public bool isMove; //移動許可
    public bool[] isRot; //移動(回転)方向
    public bool isPress; //ジャンプ入力
    public bool isJump; //ジャンプ許可

    private Rigidbody2D _rb2d;
    //private Vector2 _gururinGravity;
    public float[] gravityScale;
    private CriAtomSource _jumpSE; //ジャンプの効果音

    public GearGimmick nowGearGimiick = null;

    private Gamecontroller gameController;
    private FlagManager flagManager;

    public GanGanKamen.BossHand nowBossHand = null; //現在接触しているギミック
    public Animator animator;　//ぐるりんのアニメーター　ボスイベント用
    public bool balloonSet = false; //バルーン装備しているかどうか
    [SerializeField] private Animator balloonAnim;
    public bool finishMode = false; //ボスにとどめを刺す


    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GameObject.Find("Gururin").GetComponent<Rigidbody2D>();
        _jumpSE = GetComponent<CriAtomSource>();
        gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();

        setSpeed = true;
        isMove = true;
        isJump = false;

        if (animator != null) animator.enabled = false;

        //中間地点が設定されたときのスタート位置
        if (RemainingLife.waypoint)
        {
            transform.position = RemainingLife.startPos;
        }

        balloonSet = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Jump") && isMove)
        {
            //Jumpタグと接触時にジャンプを可能にする
            isJump = true;

            if (nowGearGimiick == null)
            {
                setSpeed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jump") && isJump)
        {
            isJump = false;
            gameController.isFlick = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //速度を毎回上書き
        if (setSpeed)
        {
            speed[0] = gameController.angle * gameController.sensitivity * 0.2f;
            speed[1] = 0.0f;
        }

        //returnGravityがTrueの時、重力を通常に戻す
        if (flagManager.returnGravity)
        {
            /*
            _gururinGravity = new Vector2(0.0f, -9.81f);
            _rb2d.AddForce(_gururinGravity);
            */
            Physics2D.gravity = new Vector2(0.0f, -9.81f);
            flagManager.isStick = false;
        }
        //それ以外の時は重力を変化
        else if(flagManager.returnGravity == false)
        {
            /*
            _gururinGravity = new Vector2(gravityScale[0], gravityScale[1]);
            _rb2d.AddForce(_gururinGravity);
            */
            Physics2D.gravity = new Vector2(gravityScale[0], gravityScale[1]);
        }

        //GearGimmickと接触していないとき
        if (nowGearGimiick == null && gameController.isPress && isMove)
        {
            //右へ移動
            if (gameController.AxB.z < 0)
            {
                isRot[0] = true;
            }

            //左へ移動
            else if (gameController.AxB.z > 0)
            {
                isRot[1] = true;
            }

            //ぐるりんの速度が一定以下かつ張り付くラックと接触していないときに初速を与える
            if(_rb2d.velocity.x > -1 && _rb2d.velocity.x < 1 && flagManager.isStick == false)
            {
                speed[0] = gameController.angle * 0.4f;
            }
        }
        if (!finishMode)
        {
            MoveCtrl();
            if (isPress)
            {
                FlickJump();
                isPress = false;
            }
        }
    }

    /// <summary>
    /// ぐるりんの移動速度
    /// </summary>
    public void GururinMove(float moveSpeedX, float moveSpeedY)
    {
        Vector2 force = new Vector2(moveSpeedX, moveSpeedY);
        _rb2d.AddForce(force);
    }

    private void MoveCtrl()
    {
        if (flagManager.moveStop)
        {
            if (flagManager.velXFixed)
            {
                //横方向の速度のみ0にする
                _rb2d.velocity = new Vector2(0.0f, _rb2d.velocity.y);
            }
            else
            {
                //速度を0にする
                _rb2d.velocity = Vector2.zero;
            }

            //加速度を0にする
            gameController.angle = 0.0f;

            //角度を固定する
            _rb2d.angularVelocity = 0.0f;
        }
        else
        {
            if (isRot[0] && !finishMode)
            {
                //reverseDirectionがTrueならコントローラーの回転方向に対してぐるりんの移動方向を反転させる
                switch (flagManager.reverseDirection)
                {
                    case true:
                        GururinMove(-speed[0], speed[1]);
                        break;

                    case false:
                        GururinMove(speed[0], speed[1]);
                        break;
                }

                isRot[0] = false;
            }
            else if (isRot[1] && !finishMode)
            {
                //左右のラックに張り付いていないとき
                if (flagManager.isMove_VG[1] == false && flagManager.isMove_VG[2] == false)
                {
                    switch (flagManager.reverseDirection)
                    {
                        case true:
                            GururinMove(speed[0], speed[1]);
                            break;

                        case false:
                            GururinMove(-speed[0], speed[1]);
                            break;
                    }
                }
                //左右のラックに張り付いているとき
                else if (flagManager.isMove_VG[1] || flagManager.isMove_VG[2])
                {
                    GururinMove(speed[0], -speed[1]);
                }

                isRot[1] = false;
            }
        }
    }

    /// <summary>
    /// ぐるりんのジャンプの強さ
    /// </summary>
    public void GururinJump(float jumpPowerX, float jumpPowerY, float jumpSpeed)
    {
        Vector2 jumpForce = new Vector2(jumpPowerX, jumpPowerY);
        _rb2d.AddForce(jumpForce * jumpSpeed);

        _jumpSE.Play();
        if (nowGearGimiick == null)
        {
            isJump = false;
            gameController.isFlick = false;
        }
        else
        {
            isMove = true;
            nowGearGimiick.Separation();
        }
    }

    private void FlickJump()
    {
        //GearGimmickとくっついているとき
        if (nowGearGimiick != null)
        {
            //フワーディアンの腕歯車とくっついているとき
            if(nowBossHand == null)
            {
                if (!gameController.isFlick)
                {
                    isPress = false;
                    return;
                }
                if (flagManager.gururinJumpDirection)
                {
                    GururinJump(-150.0f, 150.0f, 1.0f);
                }
                else
                {
                    GururinJump(150.0f, 150.0f, 1.0f);
                }
            }
            else
            {
                GururinJump(0.0f, -1.0f, jumpSpeed);
            }
        }
        else
        {
            if (!isJump)
            {
                isPress = false;
                return;
            }
            
            //上にフリック
            if (gameController.flick_up && !gameController.flick_right && !gameController.flick_left)
            {
                GururinJump(0.0f, 1.0f, jumpSpeed);
            }
            //右にフリック
            else if (gameController.flick_up && gameController.flick_right)
            {
                GururinJump(0.3f / gameController.sensitivity, 1.0f, jumpSpeed);
            }
            //左にフリック
            else if (gameController.flick_up && gameController.flick_left)
            {
                GururinJump(-0.3f / gameController.sensitivity, 1.0f, jumpSpeed);
            }
            else
            {
                gameController.isFlick = false;
            }
        }
    }

    public void BalloonApp()
    {
        if (balloonSet) return;
        balloonAnim.SetBool("App", true);
        balloonSet = true;        
    }

    public void BalloonBreak()
    {
        if (balloonSet == false) return;
        balloonSet = false;
        balloonAnim.SetBool("App", false);
        balloonAnim.SetTrigger("Break");
    }
}
