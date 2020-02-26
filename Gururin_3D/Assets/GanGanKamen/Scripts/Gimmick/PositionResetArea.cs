using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionResetArea : MonoBehaviour
{
    [Header("Z軸")][SerializeField] private float positionZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var nowPosition = other.transform.position;
            other.transform.position = new Vector3(nowPosition.x, nowPosition.y, positionZ);
        }
    }
}
