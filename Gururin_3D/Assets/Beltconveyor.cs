using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beltconveyor : MonoBehaviour
{
    [SerializeField] private GanGanKamen.GururinBase gururinBase;
    [SerializeField] private GameObject belt, limit, player;
    [SerializeField] private int length;
    [SerializeField] public float speed;
    [SerializeField] public bool right;
    private Vector3 defaultpos;
    private int count, generate;


    

    // Start is called before the first frame update
    void Start()
    {
        belt.SetActive(false);
        if(right)
        {
            limit.transform.Translate(length + 2, 0, 0);
            for (int i = 0; i < length + 1; i++)
            {
                GameObject belts = Instantiate(belt) as GameObject;
                belts.SetActive(true);
                belts.transform.Translate(length - 1 - i, 0, 0);
            }
        }else
        {
            limit.transform.Translate(-(length + 2), 0, 0);
            for (int i = 0; i < length + 1; i++)
            {
                GameObject belts = Instantiate(belt) as GameObject;
                belts.SetActive(true);
                belts.transform.Translate(-(length - 1 - i), 0, 0);
            }
        }
        
        
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(count * speed > 90)
        {
            //generate++;
            GameObject belts = Instantiate(belt) as GameObject;
            belts.SetActive(true);
            if(right)
            {
                belts.transform.Translate(-1, 0, 0);
            }else
            {
                belts.transform.Translate(1, 0, 0);
            }
            count = 0;
        }
        else
        {
            count++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var playerRb = player.GetComponent<Rigidbody>();
        if (other.CompareTag("Player"))
        {
            //gururinBase.beltspeed = speed;
            if(Input.GetMouseButton(0) == false && playerRb.velocity.x < speed/2 && playerRb.velocity.x > -speed/2)
            {
                gururinBase.MoveStop();
            }
            player.transform.Rotate(0, 0, speed * 0.9f);
            playerRb.AddForce(1f * speed, 0, 0);
            Debug.Log("x");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("y");
        }
        
    }
}
