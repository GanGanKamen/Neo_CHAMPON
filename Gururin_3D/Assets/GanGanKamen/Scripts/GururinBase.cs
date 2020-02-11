using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GururinBase : MonoBehaviour
{
    public bool CanJump { get { return canJump; } }

    public float maxSpeed { get; set; } 
    public GameObject gear { get; set; }
    public float jumpPower { get; set; }
    public float accel { get; set; }


    private float moveAngle = 0;
    private bool canJump = false;
    [SerializeField]private float rotSpeed;
    [SerializeField] private Vector3 moveVecSpeed;
    public void SetAngle(float angle)
    {
        moveAngle += (-angle / 10f);
    }

    public void GururinMove()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rotSpeed = moveAngle * accel * Time.deltaTime;
        var realSpeed = rotSpeed;
        if (realSpeed >= maxSpeed) realSpeed = maxSpeed;
        else if (realSpeed <= -maxSpeed) realSpeed = -maxSpeed;
        moveVecSpeed = new Vector3(realSpeed, 0, 0) - rigidbody.velocity;
        rigidbody.AddForce(moveVecSpeed, ForceMode.Acceleration);
    }

    public void Jump()
    {
        if(canJump == false)return;
        var rigidbody = GetComponent<Rigidbody>();
        var force = new Vector3(0, jumpPower, 0);
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }

    public void MoveStop()
    {
        //var rigidbody = GetComponent<Rigidbody>();
        //rigidbody.velocity = Vector3.zero;
        moveAngle = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            MoveStop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jump"))
        {
            canJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jump"))
        {
            canJump = false;
        }
    }
}
