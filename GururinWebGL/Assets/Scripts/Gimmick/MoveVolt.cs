using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVolt : MonoBehaviour
{

    public float moveSpeed;

    //"Max" か "Min"にぶつかったら上下を逆にする
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Max"))
        {
            moveSpeed *= -1;
        }
        if (other.CompareTag("Min"))
        {
            moveSpeed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0.0f, moveSpeed, 0.0f);
    }
}