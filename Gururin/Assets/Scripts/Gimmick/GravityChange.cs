using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChange : MonoBehaviour
{

    private PlayerMove _playerMove;
    private Gamecontroller _gameController;
    private FlagManager _flagManager;

    private GameObject _gururin;
    private Rigidbody2D _gururinRb2d;
    [SerializeField] private GameObject tooths;

    private float rackMoveSpeed;

    //重力方向の指定
    public enum GravityType
    {
        None,
        Up,
        Right,
        Left,
    }
    public GravityType gravityType;

    // Start is called before the first frame update
    void Start()
    {
        _playerMove = GameObject.Find("Gururin").GetComponent<PlayerMove>();
        _gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        _flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();

        //GravityTypeを選択しなかった場合に忠告文を出す
        if(gravityType == GravityType.None)
        {
            Debug.LogError("GravityTypeを選択してください");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //TransformFixedのタグを取得、タグを持つオブジェクトを非表示にする
            GameObject[] traFixeds = GameObject.FindGameObjectsWithTag("TransformFixed");
            foreach (GameObject traFixed in traFixeds)
            {
                traFixed.GetComponent<CapsuleCollider2D>().enabled = false;
            }
            tooths.GetComponent<CompositeCollider2D>().isTrigger = true;

            _gururin = other.gameObject;
            _gururinRb2d = other.gameObject.GetComponent<Rigidbody2D>();

            //ぐるりんのColliderをPolygonからCircleに変更
            if (_gururin.GetComponent<PolygonCollider2D>().enabled) {
                var circle = _gururin.AddComponent<CircleCollider2D>();
                circle.radius = 1.17f;
                //回転の抵抗(摩擦)を増加
                _gururinRb2d.angularDrag = 1.5f;
            }
            _gururin.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Colliderを元に戻す
            Destroy(_gururin.GetComponent<CircleCollider2D>());
            _gururin.GetComponent<PolygonCollider2D>().enabled = true;
            _gururinRb2d.angularDrag = 0.05f;
            _gururin = null;

            tooths.GetComponent<CompositeCollider2D>().isTrigger = false;

            //重力を元に戻す
            _flagManager.returnGravity = true;
            for (int i = 0; i < _flagManager.isMove_VG.Length; i++)
            {
                _flagManager.isMove_VG[i] = false;
            }

            //速度の更新
            _playerMove.setSpeed = true;
            _playerMove.isMove = true;

            //TransformFixedを表示する
            Invoke("RegeneTransformFixed", 0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ぐるりんが接触したときに選択したGravityTypeによって重力を変更
        if (_gururin != null && gravityType != GravityType.None)
        {
            rackMoveSpeed = _gameController.angle * _gameController.sensitivity * 0.2f;

            switch (gravityType)
            {
                case GravityType.Up:
                    UpGravity();
                    break;

                case GravityType.Right:
                    RightGravity();
                    break;

                case GravityType.Left:
                    LeftGravity();
                    break;
            }
        }
    }

    //上に張り付くとき
    void UpGravity()
    {
        Gravity(0.0f, 9.81f, rackMoveSpeed, 0.0f, 0);

        if (_gameController.isPress)
        {
            //左回転で右へ移動
            if (_gameController.AxB.z > 0)
            {
                _playerMove.isRot[0] = true;
                _flagManager.moveStop = false;
            }
            //右回転で左へ移動
            else if (_gameController.AxB.z < 0)
            {
                _playerMove.isRot[1] = true;
                _flagManager.moveStop = false;
            }
        }

        if (_gameController.isFlick)
        {
            _flagManager.moveStop = false;

            //下フリックで下へジャンプ
            if (_playerMove.isMove == false && _gameController.flick_down)
            {
                _gururinRb2d.AddForce(Vector2.down * _playerMove.jumpSpeed);
                //_jumpSE.Play();
            }

            _gameController.isFlick = false;
        }
    }

    //右に張り付くとき
    void RightGravity()
    {
        //速度の更新を止める
        _playerMove.setSpeed = false;

        Gravity(9.81f, 0.0f, 0.0f, rackMoveSpeed, 1);

        if (_gameController.isPress)
        {
            //右回転で上へ移動
            if (_gameController.AxB.z < 0)
            {
                _playerMove.isRot[0] = true;
                _flagManager.moveStop = false;
            }
            //左回転で下へ移動
            else if (_gameController.AxB.z > 0)
            {
                _playerMove.isRot[1] = true;
                _flagManager.moveStop = false;
            }
        }

        if (_gameController.isFlick)
        {
            _flagManager.moveStop = false;

            //左フリックで左へジャンプ
            if (_playerMove.isMove == false && _gameController.flick_left)
            {
                Vector2 jumpForce = new Vector2(-_gameController.sensitivity, 1.0f);
                _gururinRb2d.AddForce(jumpForce * _playerMove.jumpSpeed / 1.5f);
                //_jumpSE.Play();
            }

            _gameController.isFlick = false;
        }
    }

    //左に張り付くとき
    void LeftGravity()
    {
        //速度の更新を止める
        _playerMove.setSpeed = false;

        Gravity(-9.81f, 0.0f, 0.0f, rackMoveSpeed, 2);

        if (_gameController.isPress)
        {
            //左回転で上へ移動
            if (_gameController.AxB.z > 0)
            {
                _playerMove.isRot[0] = true;
                _flagManager.moveStop = false;
            }
            //右回転で下へ移動
            else if (_gameController.AxB.z < 0)
            {
                _playerMove.isRot[1] = true;
                _flagManager.moveStop = false;
            }
        }

        if (_gameController.isFlick)
        {
            _flagManager.moveStop = false;

            //右フリックで右へジャンプ
            if (_playerMove.isMove == false && _gameController.flick_right)
            {
                Vector2 jumpForce = new Vector2(_gameController.sensitivity, 1.0f);
                _gururinRb2d.AddForce(jumpForce * _playerMove.jumpSpeed / 1.5f);
                //_jumpSE.Play();
            }

            _gameController.isFlick = false;
        }
    }

    //重力変化の処理
    void Gravity(float gravityX, float gravityY, float speedX, float speedY, int VGNUM)
    {
        _flagManager.returnGravity = false;
        //重力方向の状態、0は上方向、1は右方向、2は左方向
        _flagManager.isMove_VG[VGNUM] = true;

        //ぐるりんの重力を変化
        _playerMove.gravityScale[0] = gravityX;
        _playerMove.gravityScale[1] = gravityY;
        _flagManager.isStick = true;

        //ぐるりんの速度を重力の向きに応じて変更
        _playerMove.speed[0] = speedX;
        _playerMove.speed[1] = speedY;
        //PlayerMoveによる移動を止める
        _playerMove.isMove = false;
        _playerMove.isJump = false;
    }

    void RegeneTransformFixed()
    {
        GameObject[] traFixeds = GameObject.FindGameObjectsWithTag("TransformFixed");
        foreach (GameObject traFixed in traFixeds)
        {
            traFixed.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
}
