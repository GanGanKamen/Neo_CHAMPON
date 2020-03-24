using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuturial : MonoBehaviour
{
    [SerializeField] RectTransform pivot;
    [SerializeField] float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pivot.Rotate(0, 0, -Time.deltaTime * rotateSpeed);
    }
}
