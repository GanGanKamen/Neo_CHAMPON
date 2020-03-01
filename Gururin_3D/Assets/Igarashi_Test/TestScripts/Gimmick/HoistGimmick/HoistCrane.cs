using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巻き上げ機ギミック
/// </summary>

namespace Igarashi
{
    public class HoistCrane : MonoBehaviour
    {
        public bool Hoisting { get { return _hoisting; } } // 巻き上げているかどうかの判定

        [SerializeField] [Header("巻き上げるオブジェクト")] private GameObject hoistObject;
        [SerializeField] [Range(_lowerSpeedLimit, 2.0f)] [Header("巻き上げ速度 0.1~2.0")] private float rollUpSpeed;
        [SerializeField] [Range(_lowerSpeedLimit, 2.0f)] [Header("巻き下げ速度 0.1~2.0")] private float rollDownSpeed;
        [SerializeField] [Header("ぐるりんと歯車の回転速度")] private float rotationSpeed;

        private GameObject _Gururin;
        private Rigidbody _GururinRb;
        private Rigidbody _hoistObjRb;
        private GanGanKamen.GururinBase _gururinBase;
        private GanGanKamen.GameController _gameController;
        private int _limit; // 1:UpperLimit、-1:LowerLimit、0:NotLimit
        private const float _lowerSpeedLimit = 0.1f;
        private bool _clockwise; // コントローラーの回転方向
        private bool _collisionLimit;
        private bool _hoisting;


        // Start is called before the first frame update
        void Start()
        {
            _gameController = GameObject.Find("GameController").GetComponent<GanGanKamen.GameController>();

            if (hoistObject != null)
            {
                HoistObjectSet();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                CollisionSet(other.gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // 噛み合っているとき
            if (_Gururin != null && _gururinBase.IsAttachGimmick)
            {
                if (_collisionLimit == false)
                {
                    _limit = 0;
                }

                switch (_gameController.InputIsPress)
                {
                    // 操作入力時
                    case true:
                        // 左回転
                        if (_gameController.InputAngle > 0)
                        {
                            Rotate(true);
                            if (_clockwise == false)
                            {
                                Hoist(true);
                            }
                        }
                        // 右回転
                        else if (_gameController.InputAngle < 0)
                        {
                            Rotate(false);
                            if (_clockwise)
                            {
                                Hoist(true);
                            }
                        }
                    break;

                    // 操作入力がなければ下げる
                    case false:
                        Hoist(false);

                        // 巻き上げる方向と反対に回転
                        switch (_clockwise)
                        {
                            case true:
                                Rotate(true);
                            break;

                            case false:
                                Rotate(false);
                            break;
                        }
                    break;
                }
            }
            // 噛み合っていないとき下限でなければ下げる
            else if (_limit != -1)
            {
                Hoist(false);

                // 巻き上げる方向と反対に回転
                switch (_clockwise)
                {
                    case true:
                        Rotate(true);
                    break;

                    case false:
                        Rotate(false);
                    break;
                }
            }
        }

        private void LateUpdate()
        {
            if (_Gururin != null && _gururinBase.IsAttachGimmick)
            {
                var GururinPos = _Gururin.transform.position;
                var gearPos = transform.position;

                // ジャンプ(歯車から離れる)時の処理
                if (_gameController.InputFlick)
                {
                    Jump(GururinPos, gearPos);
                    _GururinRb.useGravity = true;
                    _gururinBase.SeparateGimmick();
                    _gururinBase = null;
                    _Gururin = null;
                }
            }
        }

        // 巻き上げるオブジェクトの設定
        void HoistObjectSet()
        {
            // 巻き上げるオブジェクトの位置確認 巻き上げ機より右なら時計回りで巻き上げ
            if (hoistObject.transform.position.x > transform.position.x)
            {
                _clockwise = true;
            }

            // 巻き上げるオブジェクトがRigidBodyを持っていなかったら付与
            if (hoistObject.GetComponent<Rigidbody>() == null)
            {
                hoistObject.AddComponent<Rigidbody>();
            }
            _hoistObjRb = hoistObject.GetComponent<Rigidbody>();
            _hoistObjRb.isKinematic = true;

            // 巻き上げるオブジェクトに衝突判定用のスクリプトを付与
            hoistObject.AddComponent<HoistObject>();
            var hoistObjCs = hoistObject.GetComponent<HoistObject>();
            hoistObjCs.hoistCrane = GetComponent<HoistCrane>();
        }

        // 接触時にぐるりんのコンポーネント取得等あれこれ
        void CollisionSet(GameObject colObj)
        {
            _Gururin = colObj.gameObject;

            _GururinRb = _Gururin.GetComponent<Rigidbody>();
            _GururinRb.velocity = Vector3.zero;
            _GururinRb.angularVelocity = Vector3.zero;
            _GururinRb.useGravity = false;

            _gururinBase = _Gururin.GetComponent<GanGanKamen.GururinBase>();
            _gururinBase.AttackToGimmick();
        }

        // 巻き上げ
        void Hoist(bool hangingDirection)
        {
            var hoistObjPos = hoistObject.transform.position;
            switch (hangingDirection)
            {
                case true:
                    _hoisting = true;
                    // 上限じゃなければ巻き上げ
                    if (_limit != 1)
                    {
                        hoistObjPos.y += Time.deltaTime * rollUpSpeed;
                        var rollUp = new Vector3(hoistObjPos.x, hoistObjPos.y, hoistObjPos.z);
                        _hoistObjRb.MovePosition(rollUp);
                    }
                break;

                case false:
                    _hoisting = false;
                    // 下限じゃなければ巻き下げ
                    if (_limit != -1)
                    {
                        // 噛み合っているときと噛み合っていないときで落下速度を変化
                        if (_gururinBase != null)
                        {
                            hoistObjPos.y -= Time.deltaTime * rollDownSpeed / 2.0f;
                        }
                        else
                        {
                            hoistObjPos.y -= Time.deltaTime * rollDownSpeed;
                        }
                        var rollDown = new Vector3(hoistObjPos.x, hoistObjPos.y, hoistObjPos.z);
                        _hoistObjRb.MovePosition(rollDown);
                    }
                break;
            }
        }

        // 回転処理
        void Rotate(bool rotationDirection)
        {
            if (_limit == 0)
            {
                switch (rotationDirection)
                {
                    case true:
                        if (_Gururin == null)
                        {
                            transform.Rotate(0.0f, 0.0f, -rotationSpeed * 2.0f);
                        }
                        else
                        {
                            transform.Rotate(0.0f, 0.0f, -rotationSpeed);
                            _Gururin.transform.Rotate(0.0f, 0.0f, rotationSpeed);
                        }
                    break;

                    case false:
                        if (_Gururin == null)
                        {
                            transform.Rotate(0.0f, 0.0f, rotationSpeed * 2.0f);
                        }
                        else
                        {
                            transform.Rotate(0.0f, 0.0f, rotationSpeed);
                            _Gururin.transform.Rotate(0.0f, 0.0f, -rotationSpeed);
                        }
                    break;
                }
            }
        }

        // ジャンプの方向
        void Jump(Vector3 GururinPos, Vector3 gearPos)
        {
            var jumpPower = _gururinBase.jumpPower / 2.0f;
            // 第一象限(右上)
            if (GururinPos.x > gearPos.x && GururinPos.y > gearPos.y)
            {
                _GururinRb.AddForce(new Vector2(jumpPower, jumpPower), ForceMode.VelocityChange);
            }
            // 第二象限(左上)
            else if (gearPos.x > GururinPos.x && GururinPos.y > gearPos.y)
            {
                _GururinRb.AddForce(new Vector2(-jumpPower, jumpPower), ForceMode.VelocityChange);
            }
            // 第三象限(左下)
            else if (gearPos.x > GururinPos.x && gearPos.y > GururinPos.y)
            {
                _GururinRb.AddForce(new Vector2(-jumpPower, -jumpPower), ForceMode.VelocityChange);
            }
            // 第四象限(右下)
            else if (GururinPos.x > gearPos.x && gearPos.y > GururinPos.y)
            {
                _GururinRb.AddForce(new Vector2(jumpPower, -jumpPower), ForceMode.VelocityChange);
            }
        }

        // 巻き上げオブジェクトの上下限接触判定
        public void CollisionLimitEnter(bool limitDirection)
        {
            switch (limitDirection)
            {
                case true:
                    _limit = 1;
                break;

                case false:
                    _limit = -1;
                break;
            }
            _collisionLimit = true;
        }

        public void CollisionLimitExit()
        {
            _collisionLimit = false;
        }
    }
}
