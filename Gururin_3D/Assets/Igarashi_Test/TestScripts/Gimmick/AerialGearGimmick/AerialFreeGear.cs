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

        private float _enterGururinVelocity; // 接触時のぐるりんのVelocity.xを格納

        // Start is called before the first frame update
        void Start()
        {
            // GearTypeがあっていなければDestroy
            if (aerialGearBase.gearType != AerialGearBase.GearType.Free)
            {
                Destroy(gameObject);
            }
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

        // 回転&減速処理
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
    }
}
