using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnGravity : MonoBehaviour
{

    private FlagManager _flagmanager;

    // Start is called before the first frame update
    void Start()
    {
        _flagmanager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _flagmanager.returnGravity = true;
        }
    }
}
