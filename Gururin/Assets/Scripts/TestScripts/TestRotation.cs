﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{

    //Rigidbody2D rb2d;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        //speed = 1.0f;
        //rb2d.rotation = 45f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, speed));
        /*
        if (Input.GetKey(KeyCode.A))
        {
            rb2d.rotation += speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, speed));
        }
        */
    }
}
