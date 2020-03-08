using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リスポーンテスト用スクリプト
/// </summary>

namespace Igarashi
{
    public class TestRespawn : MonoBehaviour
    {
        private Respawn _respawn;

        // Start is called before the first frame update
        void Start()
        {
            _respawn = GameObject.Find("RespawnManager").GetComponent<Respawn>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // 接触したらリスポーン地点にリスポーン
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                _respawn.RespawnSetting();
            }
        }
    }
}
