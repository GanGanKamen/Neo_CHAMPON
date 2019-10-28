using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プロペラに風が当たっているかどうかの判定
/// </summary>

public class PropellerGimmick : MonoBehaviour
{

    public bool hitWind;
    public float rotSpeed;

    private void Start()
    {
        rotSpeed = 0.0f;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wind"))
        {
            hitWind = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wind"))
        {
            hitWind = false;
        }
    }

    private void Update()
    {
        //風が当たったら羽を回転
        if (hitWind)
        {
            rotSpeed += 0.05f;

            if(rotSpeed >= 5.0f)
            {
                rotSpeed = 5.0f;
            }

            transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed));
        }
    }
}
