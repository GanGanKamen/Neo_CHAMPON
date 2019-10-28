using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ラック張り付き時にぐるりんの位置を固定
/// </summary>

public class TransformFixed : MonoBehaviour
{

    private CriAtomSource _gearMesh;
    public GameObject gearPos;

    private FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        _gearMesh = GetComponent<CriAtomSource>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //効果音
            _gearMesh.Play();
            //ぐるりんのRigidBody2Dを取得
            var _gururinRb2d = other.GetComponent<Rigidbody2D>();
            //ぐるりんの位置を固定
            _gururinRb2d.velocity = Vector2.zero;
            _gururinRb2d.position = gearPos.transform.position;
            //ぐるりんの角度を固定
            _gururinRb2d.rotation = gearPos.transform.rotation.eulerAngles.z;
            //ぐるりんの動きを止める
            flagManager.moveStop = true;
            flagManager.VGcol = true;
        }
    }
}
