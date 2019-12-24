using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    [SerializeField] private float endurance;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private float shakeSpeed;
    [SerializeField] private float shakeWidth;
    [SerializeField] private float fallSpeed;

    private float[] startPosx;
    private float[] direction;
    private Vector3[] startPos;

    [SerializeField] private float startShakeWaitTime;
    private float startShakeCount;
    [SerializeField] private Collider2D[] colliders;
    private float recovery;

    public enum Status
    {
        Normal,
        Ready,
        Shake,
        Fall
    }
    public Status status;
    // Start is called before the first frame update
    void Start()
    {
        status = Status.Normal;
        startPosx = new float[targets.Length];
        direction = new float[targets.Length];
        startPos = new Vector3[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            startPosx[i] = targets[i].transform.localPosition.x;
            direction[i] = -1;
            startPos[i] = targets[i].transform.position;
        }
        recovery = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case Status.Normal:
                break;
            case Status.Ready:
                Ready();
                break;
            case Status.Shake:
                Shaking(shakeSpeed, shakeWidth);
                break;
            case Status.Fall:
                Falling(fallSpeed);
                break;
        }

        if(endurance <= 0)
        {
            status = Status.Fall;
            for(int i= 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
        }

        if(startShakeCount >= startShakeWaitTime && status == Status.Ready)
        {
            status = Status.Shake;
            startShakeCount = 0;
        }

        if(recovery >= 5f)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                colliders[i].enabled = true;
                targets[i].transform.position = startPos[i];
            }
            status = Status.Normal;
        }
    }

    private void Shaking(float speed, float width)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (direction[i] == -1)
            {
                //targets[i].transform.Translate(-speed*Time.deltaTime, 0, 0);
                targets[i].transform.localPosition -= new Vector3(speed * Time.deltaTime, 0, 0);
                if (targets[i].transform.localPosition.x <= startPosx[i] - width)
                {
                    direction[i] = 1;
                }
            }
            else if (direction[i] == 1)
            {
                //targets[i].transform.Translate(speed * Time.deltaTime, 0, 0);
                targets[i].transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
                if (targets[i].transform.localPosition.x >= startPosx[i] + width)
                {
                    direction[i] = -1;
                }
            }
        }

        endurance -= Time.deltaTime;
    }

    private void Ready()
    {
        startShakeCount += Time.deltaTime;
    }

    private void Falling(float speed)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].transform.localPosition -= new Vector3(0, speed * Time.deltaTime, 0);
        }
        recovery += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && status == Status.Normal)
        {
            status = Status.Ready;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && status == Status.Ready)
        {
            status = Status.Normal;
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Died") && status == Status.Fall)
        {
            for(int i = 0; i < targets.Length; i++)
            {
                targets[i].SetActive(false);
            }
            Destroy(this);
        }
    }*/
}
