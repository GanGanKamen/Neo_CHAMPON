using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GururinBase : MonoBehaviour
{
    public float speed { get; set; } 
    public GameObject gear { get; set; }
    public float jumpPower { get; set; }
    private float moveAngle = 0;


    public void SetAngle(float angle)
    {
        moveAngle += (-angle / 10f);
    }

    public void GururinMove()
    {
        var rigidbody = GetComponent<Rigidbody>();
        var rotSpeed = moveAngle * speed * Time.deltaTime;
        var moveVecSpeed = new Vector3(rotSpeed, 0, 0) - rigidbody.velocity;
        rigidbody.AddForce(moveVecSpeed, ForceMode.Acceleration);
    }

    public void Jump()
    {
        var rigidbody = GetComponent<Rigidbody>();
        var force = new Vector3(0, jumpPower, 0);
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }

    public void MoveStop()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        moveAngle = 0;
    }
}
