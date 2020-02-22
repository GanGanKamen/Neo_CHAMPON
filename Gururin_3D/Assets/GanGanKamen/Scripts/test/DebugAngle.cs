using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAngle : MonoBehaviour
{
    [SerializeField] private Transform[] transform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var angle = GetAngle(transform[0].position, transform[1].position);
        Debug.Log("Angle = " + angle);
    }

    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}
