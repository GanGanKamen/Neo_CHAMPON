using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ピストンギミックによる押し出し判定
/// </summary>

namespace Igarashi
{
    public class Extrusion : MonoBehaviour
    {
        [SerializeField] [Header("押し出す力")] private float extrusionPower;
        [SerializeField] private Piston piston;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>() && piston.Pushing)
            {
                var gururinBase = other.gameObject.GetComponent<GanGanKamen.GururinBase>();
                // 移動操作停止
                gururinBase.AttackToGimmick();

                var GururinRb = other.gameObject.GetComponent<Rigidbody>();
                GururinRb.velocity = Vector3.zero;
                GururinRb.angularVelocity = Vector3.zero;
                // 押し出しされるようにする
                GururinRb.constraints = RigidbodyConstraints.FreezeRotationX;

                // 押し出し
                var extrusionForce = new Vector3(0.0f, 0.0f, -extrusionPower);
                GururinRb.AddForce(extrusionForce, ForceMode.VelocityChange);
            }
        }
    }
}