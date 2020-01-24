using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackBall : MonoBehaviour
{
    public Vector3 force;
    private Rigidbody2D rigidbody;
    private Vector3 startPos;
    public Vector3 forceDirection;
    public float speed;
    private bool isRef = false;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
        startPos = transform.position;
        forceDirection = force.normalized * speed;
        rigidbody.AddForce(forceDirection);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Ref"))
        {
            var nowPos = transform.position;
            RefWall refWall = collision.GetComponent<RefWall>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(refWall.Reflection(startPos, nowPos) * speed);
            startPos = nowPos;
            isRef = true;
            spriteRenderer.color = Color.yellow;
        }
        else if (collision.CompareTag("Spider") && isRef)
        {
            ChaseBoss boss = collision.GetComponent<ChaseBoss>();
            boss.isDown = true;
            Destroy(gameObject);
        }

        else if (collision.CompareTag("Player"))
        {
            GameObject.Find("BossEvent").GetComponent<gururin_dead>().Dead();
            Destroy(gameObject);
        }
    }

}
