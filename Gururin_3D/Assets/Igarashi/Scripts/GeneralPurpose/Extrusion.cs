﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 押し出し動作処理
/// </summary>

namespace Igarashi
{
    public class Extrusion : MonoBehaviour
    {
        [SerializeField] private Piston piston;
        [SerializeField] [Header("押し出す力")] private float extrusionPower;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                var GururinRb = other.GetComponent<Rigidbody>();
                var playerFace = other.GetComponentInChildren<PlayerFace>();
                var gururinBase = other.GetComponent<GanGanKamen.GururinBase>();
                var extrusionForce = new Vector3();

                // ピストンによる押し出し
                if (piston != null)
                {
                    if (piston.HasPushed == false) return;

                    // 移動操作停止
                    gururinBase.AttackToGimmick();

                    GururinRb.velocity = Vector3.zero;
                    GururinRb.angularVelocity = Vector3.zero;

                    extrusionForce = new Vector3(0.0f, 0.0f, -extrusionPower);
                }
                // 巻き上げオブジェクトによる押し出し
                // ぐるりんが地面に埋まる可能性はあるので念のためRespwanZoneを配置しておくと〇
                else
                {
                    if (gururinBase.CanJump == false) return;

                    extrusionPower *= transform.parent.lossyScale.x;
                    var GururinPosX = other.transform.position.x;
                    var extrusionPosX = transform.position.x;

                    if (GururinPosX < extrusionPosX)
                    {
                        extrusionForce = GururinPosX > extrusionPosX * -1.06f ? new Vector3(-extrusionPower * 1.7f, 0.0f) : new Vector3(-extrusionPower, 0.0f);
                    }
                    else if (GururinPosX >= extrusionPosX)
                    {
                        extrusionForce = GururinPosX < extrusionPosX * -1.06f ? new Vector3(extrusionPower * 1.7f, 0.0f) : new Vector3(extrusionPower, 0.0f);
                    }
                }
                playerFace.Surprise();
                // 押し出し
                GururinRb.AddForce(extrusionForce, ForceMode.VelocityChange);
            }
        }
    }
}