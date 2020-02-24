using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Igarashi
{
    public class Extrusion : MonoBehaviour
    {
        [SerializeField] [Header("押し出す力")] private float extrusionPower;

        private bool _rangeHit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>() && _rangeHit)
            {
                var GururinRb = other.gameObject.GetComponent<Rigidbody>();
                GururinRb.velocity = Vector3.zero;
                GururinRb.angularVelocity = Vector3.zero;
                //押し出しされるようにする
                GururinRb.constraints = RigidbodyConstraints.FreezeRotationX |
                                                       RigidbodyConstraints.FreezePositionY;

                var extrusionForce = new Vector3(0.0f, 0.0f, -extrusionPower);
                GururinRb.AddForce(extrusionForce, ForceMode.VelocityChange);
            }
        }

        // PistonRangeにぐるりんがいるかどうかの判定
        public void RangeHit(bool hit)
        {
            switch (hit)
            {
                case true:
                    _rangeHit = true;
                    break;

                case false:
                    _rangeHit = false;
                    break;
            }
        }
    }
}