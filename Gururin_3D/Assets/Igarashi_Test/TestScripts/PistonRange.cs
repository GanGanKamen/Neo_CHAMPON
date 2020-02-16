using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ピストンの押し出し判定
/// </summary>
public class PistonRange : MonoBehaviour
{
    [SerializeField] private Piston _piston;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCtrl>())
        {
            _piston.RangeHit(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCtrl>())
        {
            _piston.RangeHit(false);
        }
    }
}
