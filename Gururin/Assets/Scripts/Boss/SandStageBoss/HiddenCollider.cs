using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var gururinCollider = other.gameObject.GetComponent<PolygonCollider2D>();
            //不具合起こすかもしれないので様子見実装
            gururinCollider.isTrigger = true;
        }
    }
}
