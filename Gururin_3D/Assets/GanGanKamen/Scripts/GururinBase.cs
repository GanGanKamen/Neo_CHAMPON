using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GururinBase : MonoBehaviour
{
    public bool CanJump { get { return canJump; } }
    public bool IsAttachGimmick { get { return isAttachGimmick; } }
    public bool IsCollideWall { get { return isCollideWall; } }

    public float maxSpeed { get; set; } 
    public GameObject gear { get; set; }
    public float jumpPower { get; set; }
    public float brakePower { get; set; }
    public float accel { get; set; }


    private float moveAngle = 0;
    private bool canJump = false;
    private bool isAttachGimmick = false;
    private bool isCollideWall = false;

    public void SetAngle(float angle)
    {
        moveAngle += (-angle / 10f);
    }

    public void GururinMove()
    {
        if (isAttachGimmick) return;
        var rigidbody = GetComponent<Rigidbody>();
        var rotSpeed = moveAngle * accel * Time.deltaTime;
        var realSpeed = rotSpeed;
        if (realSpeed >= maxSpeed) realSpeed = maxSpeed;
        else if (realSpeed <= -maxSpeed) realSpeed = -maxSpeed;
        var moveVecSpeed = new Vector3(realSpeed, 0, 0) - rigidbody.velocity;
        rigidbody.AddForce(moveVecSpeed, ForceMode.Acceleration);
    }

    public void Jump()
    {
        if(canJump == false || isAttachGimmick)return;
        var rigidbody = GetComponent<Rigidbody>();
        var force = new Vector3(0, jumpPower, 0);
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }

    public void AttackToGimmick()
    {
        isAttachGimmick = true;
        moveAngle = 0;
    }

    public void SeparateGimmick()
    {
        isAttachGimmick = false;
    }

    public void Brake()
    {
        moveAngle = 0;
        var rigidbody = GetComponent<Rigidbody>();
        var velocity = Mathf.Abs(rigidbody.velocity.x);
        var brakeForce = Vector3.Scale(rigidbody.velocity, new Vector3(-0.01f * brakePower / velocity, 0, 0));
        rigidbody.AddForce(brakeForce, ForceMode.VelocityChange);
    }

    public void StandStill()
    {
        moveAngle = 0;
    }

    public void MoveStop()
    {
        moveAngle = 0;
        var rigidbody = GetComponent<Rigidbody>();
        var frictionalForce = Vector3.Scale(rigidbody.velocity, new Vector3(-1, 0, 0));
        rigidbody.AddForce(frictionalForce, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            MoveStop();
            isCollideWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isCollideWall = false;
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
