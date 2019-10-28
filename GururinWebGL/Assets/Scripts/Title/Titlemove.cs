using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titlemove : MonoBehaviour
{
    public Titlecontroller titlecontroller;
    public int terms1;
    public int movecount;
    public bool moveactive;

    private Rigidbody2D _rb2d;
    private CriAtomSource _jumpSE;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント読み込み
        _rb2d = GameObject.Find("Gururin").GetComponent<Rigidbody2D>();
        _jumpSE = GameObject.Find("SE_jump(CriAtomSource)").GetComponent<CriAtomSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(titlecontroller.isActive)
        {
            terms1 = Random.Range(0, 3);
            movecount = 0;

            if (terms1 == 0 || terms1 == 1)
            {
                transform.position = new Vector2(Random.Range(-4.0f,4.0f), 6.0f);
            }
            else if (terms1 == 2)
            {
                transform.position = new Vector2(-9.9f, 3.0f);
            }
            _rb2d.isKinematic = false;
            titlecontroller.isActive = false;
        }

        if(terms1 == 0)
        {
            movecount++;
            if (movecount >= 87)
            {
                Vector2 force = new Vector2(2.0f, 0.0f);
                _rb2d.AddForce(force);
            }
        }
        else if(terms1 == 1)
        {
            movecount++;
            if(movecount >= 87)
            {
                Vector2 force = new Vector2(-2.0f, 0.0f);
                _rb2d.AddForce(force);
            }
        }
        else if(terms1 == 2)
        {
            movecount++;
            Vector2 force = new Vector2(2.0f, 0.0f);
            _rb2d.AddForce(force);
            int terms2 = Random.Range(0, 2);
            if(terms2 == 0)
            {
                if (movecount == 150)
                {
                    float jump = 350.0f;
                    _rb2d.AddForce(Vector2.up * jump);
                    _jumpSE.Play();
                }
            }
            /*else if(terms2 == 1)
            {
                if (movecount == 193)
                {
                    float jump = 350.0f;
                    _rb2d.AddForce(Vector2.up * jump);
                }
            }*/
        }

        if (transform.position.x < -10 ||
            transform.position.x > 10 ||
            transform.position.y < -6)
        {
            _rb2d.isKinematic = true;
            _rb2d.velocity = Vector2.zero;
            titlecontroller.isCheck = true;
            movecount = 0;

        }
    }
}
