using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巻き上げ機ギミックの動作処理
/// </summary>

namespace Igarashi
{
    public class HoistCrane : MonoBehaviour
    {
        public bool HasHoisted { get { return _hasHoisted; } } // 巻き上げているかどうかの判定
        public bool HasHoistObjRb { get { return _hasHoistObjRb; } }

        [SerializeField] [Header("巻き上げるオブジェクト")] private GameObject hoistObject;
        [SerializeField] [Range(_lowerSpeedLimit, 2.0f)] [Header("巻き上げ速度 0.1~2.0")] private float rollUpSpeed;
        [SerializeField] [Range(_lowerSpeedLimit, 2.0f)] [Header("巻き下げ速度 0.1~2.0")] private float rollDownSpeed;
        [SerializeField] [Header("ぐるりんと歯車の回転速度")] private float rotationSpeed;

        private GameObject _Gururin;
        private Rigidbody _GururinRb;
        private Rigidbody _hoistObjRb;
        private PlayerFace _playerFace;
        private GanGanKamen.GururinBase _gururinBase;
        private GanGanKamen.GameController _gameController;
        private int _limit; // 1:UpperLimit、-1:LowerLimit、0:NotLimit
        private const float _lowerSpeedLimit = 0.1f;
        private bool _isClockwise; // 巻き上げる回転方向
        private bool _hasLimitCollided;
        private bool _hasHoisted;
        private bool _hasHoistObjRb;


        // Start is called before the first frame update
        void Start()
        {
            _gameController = GameObject.Find("GameController").GetComponent<GanGanKamen.GameController>();

            if (hoistObject != null)
            {
                HoistObjectSettings();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                CollisionSettings(other.gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // 噛み合っているとき
            if (_Gururin != null)
            {
                if (_hasLimitCollided == false)
                {
                    _limit = 0;
                }

                switch (_gameController.InputIsPress)
                {
                    // 操作入力時
                    case true:
                        ControllerOperation();
                        break;

                    // 操作入力がなければ下げる
                    case false:
                        _playerFace.Nomal();
                        Hoist(false);
                        Rotate(_isClockwise);
                        break;
                }
            }
            // 噛み合っていないとき
            else
            {
                // LowerLimit(下限)と接触していなければLowerLimitまで下げる
                if (_limit != -1)
                {
                    _limit = 0;
                    Hoist(false);
                    Rotate(_isClockwise);
                }
            }
        }

        private void LateUpdate()
        {
            if (_Gururin == null) return;

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

        // 巻き上げるオブジェクトの設定
        void HoistObjectSettings()
        {
            // 巻き上げるオブジェクトの位置確認 巻き上げ機より右なら時計回りで巻き上げ
            if (hoistObject.transform.position.x > transform.position.x)
            {
                _isClockwise = true;
            }

            if (hoistObject.GetComponentInChildren<AerialGearBase>() == null)
            {
                // 巻き上げるオブジェクトがRigidBodyを持っていなかったら付与
                _hoistObjRb = hoistObject.GetComponent<Rigidbody>() == null ? hoistObject.AddComponent<Rigidbody>() : hoistObject.GetComponent<Rigidbody>();
                _hoistObjRb.isKinematic = true;
            }
            _hasHoistObjRb = _hoistObjRb == null ? false : true;

            // 巻き上げるオブジェクトに衝突判定用のスクリプトを付与
            hoistObject.AddComponent<HoistObject>();
            var hoistObjCs = hoistObject.GetComponent<HoistObject>();
            hoistObjCs.hoistCrane = GetComponent<HoistCrane>();
        }

        // 接触時にぐるりんのコンポーネント取得等あれこれ
        void CollisionSettings(GameObject colObj)
        {
            _Gururin = colObj.gameObject;
            _playerFace = _Gururin.GetComponentInChildren<PlayerFace>();

            _GururinRb = _Gururin.GetComponent<Rigidbody>();
            _GururinRb.velocity = Vector3.zero;
            _GururinRb.angularVelocity = Vector3.zero;
            _GururinRb.useGravity = false;

            _gururinBase = _Gururin.GetComponent<GanGanKamen.GururinBase>();
            _gururinBase.AttackToGimmick();
        }

        // コントローラーの回転操作
        private void ControllerOperation()
        {
            if (_gameController.InputLongPress)
            {
                _playerFace.Angry();
            }

            // 左回転
            if (_gameController.InputAngle > 0.0f)
            {
                _playerFace.Nomal();
                Rotate(true);
                if (_isClockwise == false)
                {
                    Hoist(true);
                }
            }
            // 右回転
            else if (_gameController.InputAngle < 0.0f)
            {
                _playerFace.Nomal();
                Rotate(false);
                if (_isClockwise)
                {
                    Hoist(true);
                }
            }
            else if (_gameController.InputAngle == 0.0f)
            {
                return;
            }
        }

        // 巻き上げ
        void Hoist(bool hangingDirection)
        {
            var hoistObjPos = hoistObject.transform.position;
            switch (hangingDirection)
            {
                case true:
                    // 上限じゃなければ巻き上げ
                    if (_limit != 1)
                    {
                        hoistObjPos.y += Time.deltaTime * rollUpSpeed;
                    }
                    break;

                case false:
                    // 下限じゃなければ巻き下げ
                    if (_limit != -1)
                    {
                        // 噛み合っているときと噛み合っていないときで落下速度を変化
                        hoistObjPos.y -= _Gururin == null ? Time.deltaTime * rollDownSpeed / 2.0f : Time.deltaTime * rollDownSpeed / 4.0f;
                    }
                    break;
            }
            _hasHoisted = hangingDirection;
            if (_hoistObjRb != null)
            {
                _hoistObjRb.MovePosition(hoistObjPos);
            }
            else
            {
                hoistObject.transform.position = hoistObjPos;
            }
        }

        // 回転処理
        void Rotate(bool rotationDirection)
        {
            if (_limit != 0) return;

            // 噛み合っているときと噛み合っていないときで回転速度を変化
            var baseGearRotationSpeed = _Gururin == null ? rotationSpeed * 2.0f : rotationSpeed;
            switch (rotationDirection)
            {
                case true:
                    transform.Rotate(0.0f, 0.0f, -baseGearRotationSpeed);
                    if (_Gururin != null)
                    {
                        _Gururin.transform.Rotate(0.0f, 0.0f, rotationSpeed);
                    }
                    break;

                case false:
                    transform.Rotate(0.0f, 0.0f, baseGearRotationSpeed);
                    if (_Gururin != null)
                    {
                        _Gururin.transform.Rotate(0.0f, 0.0f, -rotationSpeed);
                    }
                    break;
            }
        }

        // ジャンプの方向
        void Jump(Vector3 GururinPos, Vector3 gearPos)
        {
            var jumpPower = _gururinBase.jumpPower;
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
            _limit = limitDirection ? 1 : -1;
            _hasLimitCollided = true;
        }

        public void CollisionLimitExit()
        {
            _hasLimitCollided = false;
        }
    }
}
