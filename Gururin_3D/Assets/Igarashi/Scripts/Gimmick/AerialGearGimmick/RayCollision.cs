using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rayCollisionMesh = GetComponent<MeshRenderer>();
        rayCollisionMesh.enabled = false;
    }
}
