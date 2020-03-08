using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 即死ギミック
/// </summary>

namespace Igarashi
{
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] [Header("電流棒・迫りくる炎時のみ選択")] private bool deadMode;

        private Respawn _respawn;
        private string _nowSceneName;

        // Start is called before the first frame update
        void Start()
        {
            if (deadMode == false)
            {
                _respawn = GameObject.Find("RespawnManager").GetComponent<Respawn>();
            }
            _nowSceneName = SceneManager.GetActiveScene().name;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                switch (deadMode)
                {
                    case true:
                        // ☆死亡演出

                        // シーンをリロード
                        SceneManager.LoadScene(_nowSceneName);
                        break;

                    case false:
                        // リスポーン地点にリスポーン
                        _respawn.RespawnSetting();
                        break;
                }
            }
        }
    }
}