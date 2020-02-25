using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRespawn : MonoBehaviour
{
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] [Header("ぐるりんの固定Z軸")] private float fixedZPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
        {
            var gururinBase = other.gameObject.GetComponent<GanGanKamen.GururinBase>();
            // 移動操作が停止されていたら再開メソッドを呼び出し
            if (gururinBase.IsAttachGimmick)
            {
                gururinBase.SeparateGimmick();
            }

            var GururinRb = other.gameObject.GetComponent<Rigidbody>();
            // FreezePosition、FreezeRotationを再設定
            GururinRb.constraints = RigidbodyConstraints.FreezePositionZ |
                                                   RigidbodyConstraints.FreezeRotationX |
                                                   RigidbodyConstraints.FreezeRotationY;

            // 角度と位置を初期化
            other.transform.rotation = Quaternion.Euler(Vector3.zero);
            other.transform.position = new Vector3(respawnPoint.transform.position.x, respawnPoint.transform.position.y, fixedZPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
