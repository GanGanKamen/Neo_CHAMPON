using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巻き上げオブジェクトの限界値判定用空スクリプト
/// </summary>

namespace Igarashi
{
    public class HoistLimit : MonoBehaviour
    {
        [SerializeField] private HoistCrane _hoistCrane;
        [SerializeField] [Multiline(2)] private string memo = "巻き上げオブジェクトの限界値に設定したい\n" +
                                                                                        "場所にこのオブジェクトを配置してください";

        // Start is called before the first frame update
        void Start()
        {
            if(_hoistCrane != null)
            {
                if (_hoistCrane.HasHoistObjRb == false)
                {
                    gameObject.AddComponent<Rigidbody>();
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

            var limitPosMesh = GetComponent<MeshRenderer>();
            limitPosMesh.enabled = false;
        }
    }
}
