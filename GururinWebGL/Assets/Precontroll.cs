using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Precontroll : MonoBehaviour
{
    public Gamecontroller gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //gameController.prectrl = true;
        }
    }
}
