using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リスポーン地点とぐるりんの接触判定
/// </summary>

namespace Igarashi
{
    public class RespawnPoint : MonoBehaviour
    {
        private Respawn _respawn;
        [SerializeField] [Multiline(2)] private string memo = "リスポーン地点に設定したい場所に\nこのオブジェクトを配置してください";

        // Start is called before the first frame update
        void Start()
        {
            _respawn = GameObject.Find("RespawnSet").GetComponent<Respawn>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                // ぐるりんと接触したらリスポーン地点に設定
                _respawn.RespawnPointSet(gameObject);
            }
        }
    }
}
