using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefWall : MonoBehaviour
{
    [SerializeField] private Transform normalP1, normalP2, normalP3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Reflection(Vector3 sPos, Vector3 hitPos)
    {
        var vecF = (hitPos - sPos).normalized;
        var normalVec = new Vector3();
        if (Vector3.Distance(sPos, normalP2.position) < Vector3.Distance(sPos, normalP3.position))
        {
            normalVec = (normalP2.position - normalP1.position).normalized;
        }
        else
        {
            normalVec = (normalP3.position - normalP1.position).normalized;
        }
        var a = Vector3.Dot(-vecF, normalVec);
        var vecP = vecF + normalVec * a;
        var vecR = vecF + 2 * a * normalVec;
        return vecR;
    }
}
