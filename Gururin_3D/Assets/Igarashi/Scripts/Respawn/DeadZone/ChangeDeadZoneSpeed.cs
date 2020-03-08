using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDeadZoneSpeed : MonoBehaviour
{
    [SerializeField] [Header("即死ゾーンの移動速度を変更")] [Range(0.1f, 2.0f)] private float changeSpeed;
    [SerializeField] [Multiline(2)] private string memo = "即死ゾーンの速度を変更したい場所に\n" +
                                                                                     "このオブジェクトを配置してください";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MoveDeadZone>())
        {
            var moveDeadZone = other.GetComponent<MoveDeadZone>();
            moveDeadZone.SpeedSetting(changeSpeed);
        }
    }
}
