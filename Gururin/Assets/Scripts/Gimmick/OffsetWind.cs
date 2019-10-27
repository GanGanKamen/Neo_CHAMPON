using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遮られている風の切り替え
/// </summary>

public class OffsetWind : MonoBehaviour
{
    public bool offset;
    public GameObject[] wind;

    // Update is called once per frame
    void Update()
    {
        if (offset)
        {
            wind[0].SetActive(true);

            wind[1].SetActive(false);
        }
        else if (offset == false)
        {
            wind[0].SetActive(false);

            wind[1].SetActive(true);
        }
    }
}
