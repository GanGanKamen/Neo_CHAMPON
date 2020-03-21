using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSilverGoal : MonoBehaviour
{
    [SerializeField] [Header("移動先のPosition")] private Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GanGanKamen.PlayerCtrl>())
        {
            other.gameObject.transform.position = destination.position;
            other.gameObject.GetComponent<GanGanKamen.GururinBase>().StandStill();
            var GururinRb = other.GetComponent<Rigidbody>();
            GururinRb.velocity = Vector3.zero;
            GururinRb.angularDrag = 0.0f;
        }
    }
}
