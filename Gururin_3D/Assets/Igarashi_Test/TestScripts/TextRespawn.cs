using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRespawn : MonoBehaviour
{
    [SerializeField] private GameObject _respawnPoint;
    [SerializeField] private float fixedZPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
        {
            var _GururinRb = other.gameObject.GetComponent<Rigidbody>();
            // FreezePosition、FreezeRotationを再設定
            _GururinRb.constraints = RigidbodyConstraints.FreezePositionZ |
                                                     RigidbodyConstraints.FreezeRotationX |
                                                     RigidbodyConstraints.FreezeRotationY;

            // 角度と位置を初期化
            other.transform.rotation = Quaternion.Euler(Vector3.zero);
            other.transform.position = new Vector3(_respawnPoint.transform.position.x, _respawnPoint.transform.position.y, fixedZPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
