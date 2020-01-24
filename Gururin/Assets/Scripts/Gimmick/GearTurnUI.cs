using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearTurnUI : MonoBehaviour
{
    private bool startAnimation;
    private int direction;
    [SerializeField] private SpriteRenderer sprite;
    private int turnNum;
    [SerializeField] private float turnSpeed;
    private int rotateCount = 90;

    private GearGimmick thisGear;

    public bool moveGear = true;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        startAnimation = false;
        direction = -1;
        turnNum = 0;
        thisGear = transform.parent.GetComponent<GearGimmick>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startAnimation)
        {
            switch (turnNum)
            {
                case 1:
                    sprite.transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
                    break;
                case 2:
                    sprite.transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
                    break;
                case 3:
                    if (direction == -1)
                    {
                        rotateCount++;
                        sprite.transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
                        if (rotateCount >= 180)
                        {
                            sprite.flipX = true;
                            rotateCount = 0;
                            direction = 1;
                        }
                    }
                    else if (direction == 1)
                    {
                        rotateCount++;
                        sprite.transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
                        if (rotateCount >= 180)
                        {
                            rotateCount = 0;
                            sprite.flipX = false;
                            direction = -1;
                        }
                    }
                    break;
            }
        }
    }

    public void LeaveGear(bool direction)
    {
        moveGear = false;
        if(direction)
        {
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        }
        else
        {
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        }
        rotateCount = 0;
        startAnimation = false;
        sprite.flipX = false;
        turnNum = 0;
        sprite.gameObject.SetActive(false);
    }

    public void AttachPlayer(int _turnNum)
    {
        if (!moveGear)
        {
            moveGear = true;
            return;
        }
        rotateCount = 90;
        startAnimation = true;
        sprite.gameObject.SetActive(true);
        turnNum = _turnNum;
        if (turnNum == 2)
        {
            sprite.flipX = true;
        }
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        
    }

    public void SeparatePlayer()
    {
        rotateCount = 0;
        startAnimation = false;
        sprite.flipX = false;
        turnNum = 0;
        sprite.gameObject.SetActive(false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        
    }
}
