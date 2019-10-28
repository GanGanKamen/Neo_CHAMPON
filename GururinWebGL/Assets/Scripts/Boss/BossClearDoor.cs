using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClearDoor : MonoBehaviour
{
    public BossEvent bossEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossEvent.goBack = false;
        }
    }
}
