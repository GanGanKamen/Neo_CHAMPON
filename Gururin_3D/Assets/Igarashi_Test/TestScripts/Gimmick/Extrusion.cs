using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 押し出し動作処理
/// </summary>

namespace Igarashi
{
    public class Extrusion : MonoBehaviour
    {
        [SerializeField] [Header("押し出す力")] private float extrusionPower;
        [SerializeField] private Piston piston;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                var GururinRb = other.gameObject.GetComponent<Rigidbody>();
                var gururinBase = other.gameObject.GetComponent<GanGanKamen.GururinBase>();
                var extrusionForce = new Vector3();
                // ピストンによる押し出し
                if (piston != null && piston.Pushing)
                {
                    // 移動操作停止
                    gururinBase.AttackToGimmick();

                    //var GururinRb = other.gameObject.GetComponent<Rigidbody>();
                    GururinRb.velocity = Vector3.zero;
                    GururinRb.angularVelocity = Vector3.zero;

                    extrusionForce = new Vector3(0.0f, 0.0f, -extrusionPower);
                }
                // 巻き上げオブジェクトによる押し出し
                // 巻き上げオブジェクトによる押し出しでぐるりんが地面に埋まる可能性はあるので念のためリスポーンを配置しておくと〇
                else
                {
                    if (gururinBase.CanJump == false) return;

                    extrusionPower *= transform.parent.lossyScale.x;
                    var GururinPosX = other.transform.position.x;
                    var extrusionPosX = transform.position.x;

                    if (GururinPosX < extrusionPosX)
                    {
                        if (GururinPosX > extrusionPosX * -1.06f)
                        {
                            extrusionForce = new Vector3(-extrusionPower * 1.7f, 0.0f);
                        }
                        else
                        {
                            extrusionForce = new Vector3(-extrusionPower, 0.0f);
                        }
                    }
                    else if (GururinPosX >= extrusionPosX)
                    {
                        if (GururinPosX < extrusionPosX * 1.06f)
                        {
                            extrusionForce = new Vector3(extrusionPower * 1.7f, 0.0f);
                        }
                        else
                        {
                            extrusionForce = new Vector3(extrusionPower, 0.0f);
                        }
                    }
                }
                // 押し出し
                GururinRb.AddForce(extrusionForce, ForceMode.VelocityChange);
            }
        }
    }
}