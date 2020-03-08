using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空中自由歯車ギミックの動作処理
/// </summary>

namespace Igarashi
{
    public class AerialFreeGear : MonoBehaviour
    {
        [SerializeField] private AerialGearBase aerialGearBase;
        [SerializeField] [Header("回転への加速度 1.0~4.0")] [Range(1.0f, 4.0f)] private float acceleration;
        [SerializeField] [Header("時間経過による減速度 0.1~1.0")] [Range(0.1f, 1.0f)] private float deceleration;

        private int _direction;
        private float _enterGururinVelocity; // 接触時のぐるりんのVelocity.xを格納
        private float _pendulumSpeed;
        private float _pendulumTime;
        private float _timer;
        private bool _raySkip;

        // Start is called before the first frame update
        void Start()
        {
            // GearTypeがあっていなければDestroy
            if (aerialGearBase.gearType != AerialGearBase.GearType.Free)
            {
                Destroy(gameObject);
            }
        }

        public void FreeGearInit()
        {
            _direction = 0;
            _pendulumSpeed = 0.0f;
            _pendulumTime = 0.0f;
            _raySkip = false;
        }

        // 引力
        public void Gravitation(GameObject Gururin, GameObject baseGear, Rigidbody GururinRb, Rigidbody baseGearRb)
        {
            var coefficient = 6.67408f;
            var direction = baseGear.transform.position - Gururin.transform.position;
            var distance = direction.magnitude;
            distance *= distance;
            var gravity = coefficient * baseGearRb.mass * GururinRb.mass / distance;

            GururinRb.AddForce(gravity * direction.normalized);
        }

        // 非接触時、ぐるりんのVelocity.xを毎F取得(接触する1F前のVelocity.xを取得するため)
        public void GetGururinVelocity(Rigidbody GururinRb)
        {
            if (aerialGearBase.IsCollision == false)
            {
                if (GururinRb.velocity.x != 0.0f)
                {
                    if (GururinRb.transform.position.y > transform.position.y)
                    {
                        _enterGururinVelocity = -GururinRb.velocity.x;
                    }
                    else if (GururinRb.transform.position.y <= transform.position.y)
                    {
                        _enterGururinVelocity = GururinRb.velocity.x;
                    }
                }
            }
        }

        // 接触時の回転&減速処理
        public void RotatingGear(GameObject baseGear)
        {
            baseGear.transform.Rotate(0.0f, 0.0f, _enterGururinVelocity * acceleration);

            if (_enterGururinVelocity > 0.0f)
            {
                _enterGururinVelocity -= Time.deltaTime * deceleration;

                if (_enterGururinVelocity <= 0.0f)
                {
                    _enterGururinVelocity = 0.0f;
                }
            }
            else if (_enterGururinVelocity < 0.0f)
            {
                _enterGururinVelocity += Time.deltaTime * deceleration;

                if (_enterGururinVelocity >= 0.0f)
                {
                    _enterGururinVelocity = 0.0f;
                }
            }
        }

        // 接触時回転終了後
        public void RaySkip(GameObject Gururin, GameObject baseGear)
        {
            if (_enterGururinVelocity != 0.0f) return;

            switch (_raySkip)
            {
                case true:
                    Pendulum(baseGear, Gururin.transform.position.x);
                    break;

                case false:
                    if (_pendulumTime < 0.0f) return;

                    var rayDirection = (Gururin.transform.position - baseGear.transform.position).normalized;
                    var ray = new Ray(baseGear.transform.position, rayDirection);

                    foreach (RaycastHit hit in Physics.RaycastAll(ray, 2.0f))
                    {
                        if (hit.collider.gameObject.GetComponent<RayCollision>())
                        {
                            _raySkip = true;
                        }
                        else
                        {
                            _pendulumSpeed += Time.deltaTime * 0.2f;
                            if (Gururin.transform.position.x < baseGear.transform.position.x)
                            {
                                baseGear.transform.Rotate(0.0f, 0.0f, _pendulumSpeed);
                                _direction = 1;
                            }
                            else if (Gururin.transform.position.x > baseGear.transform.position.x)
                            {
                                baseGear.transform.Rotate(0.0f, 0.0f, -_pendulumSpeed);
                                _direction = -1;
                            }
                        }
                    }
                    break;
            }
        }

        // 振り子運動
        void Pendulum(GameObject baseGear, float GururinPosX)
        {
            if(_pendulumTime == 0.0f)
            {
                _pendulumTime = _pendulumSpeed;
                _timer = _pendulumTime;
            }

            _timer -= Time.deltaTime;
            if (_timer <= 0.0f)
            {
                _pendulumTime -= _pendulumTime / 5.0f;
                _timer = _pendulumTime;
                _pendulumSpeed = 0.0f;
                _raySkip = false;
            }

            _pendulumSpeed += Time.deltaTime * 0.2f;
            if (_direction == 1)
            {
                baseGear.transform.Rotate(0.0f, 0.0f, _pendulumSpeed);
            }
            else if (_direction == -1)
            {
                baseGear.transform.Rotate(0.0f, 0.0f, -_pendulumSpeed);
            }
        }
    }
}
