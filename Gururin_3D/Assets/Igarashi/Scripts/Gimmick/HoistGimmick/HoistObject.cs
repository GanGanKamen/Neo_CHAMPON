using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巻き上げオブジェクトの接触判定
/// </summary>

namespace Igarashi
{
    public class HoistObject : MonoBehaviour
    {
        public HoistCrane hoistCrane;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<HoistLimit>())
            {
                hoistCrane.CollisionLimitEnter(hoistCrane.HasHoisted ? true : false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<HoistLimit>())
            {
                hoistCrane.CollisionLimitExit();
            }
        }
    }
}
