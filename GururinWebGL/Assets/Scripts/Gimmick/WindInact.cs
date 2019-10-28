using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 風のアクティブ状態切り替え
/// </summary>

public class WindInact : MonoBehaviour
{
    [SerializeField] OffsetWind offsetWind;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Gimmick"))
        {
            offsetWind.offset = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Gimmick"))
        {
            offsetWind.offset = false;
        }
    }
}
