using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{

    public GameObject balloonPrefab;
    public float dropDistance;
    public bool collision;
    private Rigidbody2D _rb2d;
    private CriAtomSource _breakSE;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _breakSE = GetComponent<CriAtomSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jump"))
        {
            collision = true;
            _breakSE.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jump"))
        {
            collision = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (collision && _rb2d.velocity.y < dropDistance)
        //JumpColliderに接触したとき
        if (collision)
        {
            var pos = transform.position;
            var balloon = Instantiate(balloonPrefab);
            balloon.transform.position = new Vector2(pos.x, pos.y + 0.3f);
            gameObject.SetActive(false);
        }
    }
}
