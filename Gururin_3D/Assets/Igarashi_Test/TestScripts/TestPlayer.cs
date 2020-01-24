using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    public bool gimmickHit;

    private Rigidbody _rb2d;

    [SerializeField] private GameObject gururinFace;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        gururinFace.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (gimmickHit == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var force = new Vector3(_rb2d.velocity.x,  jumpForce * Time.deltaTime);
                _rb2d.AddForce(force, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rb2d.velocity = new Vector3(-moveSpeed * Time.deltaTime, _rb2d.velocity.y);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _rb2d.velocity = new Vector3(moveSpeed * Time.deltaTime, _rb2d.velocity.y);
            }
        }
    }
}
