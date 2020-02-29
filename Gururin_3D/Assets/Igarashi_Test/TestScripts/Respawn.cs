using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リスポーン関係の設定
/// </summary>

namespace Igarashi
{
    public class Respawn : MonoBehaviour
    {
        private GameObject _Gururin;
        [SerializeField] [Header("リスポーン地点(アタッチしなくてOK)")] private GameObject _respawnPoint;

        // Start is called before the first frame update
        void Start()
        {
            _Gururin = GameObject.Find("Player");
        }

        // ぐるりんをリスポーンしたいときに呼ぶ(仮)
        public void RespawnSet()
        {
            var gururinBase = _Gururin.gameObject.GetComponent<GanGanKamen.GururinBase>();
            // 移動操作が停止されていたら再開メソッドを呼び出し
            if (gururinBase.IsAttachGimmick)
            {
                gururinBase.SeparateGimmick();
            }

            var GururinRb = _Gururin.gameObject.GetComponent<Rigidbody>();
            // FreezeRotationを再設定
            GururinRb.constraints = RigidbodyConstraints.FreezeRotationX |
                                                   RigidbodyConstraints.FreezeRotationY;

            // 角度と位置を初期化
            _Gururin.transform.rotation = Quaternion.Euler(Vector3.zero);
            _Gururin.transform.position = new Vector3(_respawnPoint.transform.position.x, _respawnPoint.transform.position.y, 0.0f);
        }

        // リスポーン地点を設定
        public void RespawnPointSet(GameObject respawnPoint)
        {
            if (respawnPoint != _respawnPoint)
            {
                _respawnPoint = respawnPoint;
            }
        }
    }
}
