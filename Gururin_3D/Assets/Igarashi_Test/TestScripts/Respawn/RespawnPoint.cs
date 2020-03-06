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
        [SerializeField] [Header("リスポーンさせたい位置")] private GameObject respawnPos;
        [SerializeField] [Multiline(2)] private string memo = "リスポーン地点に設定したい場所に\n" +
                                                                                         "このオブジェクトを配置してください";

        private Respawn _respawn;

        // Start is called before the first frame update
        void Start()
        {
            _respawn = GameObject.Find("RespawnManager").GetComponent<Respawn>();

            if (respawnPos != null)
            {
                var respawnPosMesh = respawnPos.GetComponent<MeshRenderer>();
                respawnPosMesh.enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                // ぐるりんと接触したらリスポーン地点に設定
                _respawn.RespawnPointSetting(respawnPos);
            }
        }
    }
}
