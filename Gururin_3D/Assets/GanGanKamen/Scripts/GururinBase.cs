using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GururinBase : MonoBehaviour
{
    public float speed { get; set; } 
    public GameObject gear { get; set; }
    public float jumpPower { get; set; }
    private int preDirection;

    public void GururinMove(float angle,bool direction)
    {
        var rigidbody = GetComponent<Rigidbody>();
        var rotSpeed = angle * speed * Time.deltaTime; 
        if (direction)
        {
            rigidbody.AddForce(rotSpeed, 0, 0,ForceMode.Acceleration);
            preDirection = 1;
        }
        else
        {
            rigidbody.AddForce(-rotSpeed, 0, 0, ForceMode.Acceleration);
            preDirection = -1;
        }
    }

    public void SpeedReset()
    {
        var rigidbody = GetComponent<Rigidbody>();
        var velocity = rigidbody.velocity.x;
        Debug.Log(preDirection);
        switch (preDirection)
        {
            case 1:
                if (velocity > 0)
                {
                    velocity -= 5f * Time.deltaTime;

                }
                else
                {
                    velocity = 0;
                }
                break;
            case -1:
                if (velocity < 0)
                {
                    velocity += 5f * Time.deltaTime;

                }
                else
                {
                    velocity = 0;
                }
                break;
        }
        
        rigidbody.velocity = new Vector3(velocity,
   rigidbody.velocity.y, rigidbody.velocity.z);
    }

    public void Jump()
    {
        var rigidbody = GetComponent<Rigidbody>();
        var force = new Vector3(0, jumpPower, 0);
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
