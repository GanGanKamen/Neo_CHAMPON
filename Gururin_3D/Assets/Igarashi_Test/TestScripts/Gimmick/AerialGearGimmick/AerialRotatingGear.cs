using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空中回転歯車ギミックの動作処理
/// </summary>

namespace Igarashi
{
    public class AerialRotatingGear : MonoBehaviour
    {
        [SerializeField] private AerialGearBase aerialGearBase;
        [SerializeField] [Header("歯車の回転速度 +:反時計回り -:時計回り")] private float gearRotationSpeed;

        // Start is called before the first frame update
        void Start()
        {
            // GearTypeがあっていなければDestroy
            if (aerialGearBase.gearType != AerialGearBase.GearType.Rotating)
            {
                Destroy(gameObject);
            }
        }

        // 回転処理
        public void RotatingGear(GameObject baseGear)
        {
            baseGear.transform.Rotate(0.0f, 0.0f, gearRotationSpeed);
        }
    }
}
