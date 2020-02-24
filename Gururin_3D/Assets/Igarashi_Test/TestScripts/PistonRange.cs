using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ピストンの押し出し判定
/// </summary>

namespace Igarashi
{
    public class PistonRange : MonoBehaviour
    {
        [SerializeField] private Extrusion extrusion;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                extrusion.RangeHit(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                extrusion.RangeHit(false);
            }
        }
    }
}
