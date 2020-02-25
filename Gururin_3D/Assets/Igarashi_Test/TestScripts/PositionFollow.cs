using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollow : MonoBehaviour
{
    [SerializeField] [Header("位置を追従させるオブジェクト")] private Transform followPos;
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - followPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followPos.position + _offset;
    }
}
