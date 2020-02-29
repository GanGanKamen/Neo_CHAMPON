using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeArm : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [Header("三つのポイント")][SerializeField] private Transform startPoint, middlePoint,overPoint;

    private void Awake()
    {
        if(GetComponent<LineRenderer>() == null) gameObject.AddComponent<LineRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = 3;
        var points = new Vector3[3];
        points[0] = startPoint.position;
        points[1] = middlePoint.position;
        points[2] = overPoint.position;
        lineRenderer.SetPositions(points);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(2, overPoint.position);
    }
}
