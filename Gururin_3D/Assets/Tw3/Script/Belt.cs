using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    private Beltconveyor beltConveyor;
    // Start is called before the first frame update
    void Start()
    {
        beltConveyor = GameObject.Find("BeltconCollider").GetComponent<Beltconveyor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(beltConveyor.right)
        {
            this.gameObject.transform.Translate(0.01f * beltConveyor.speed, 0, 0);
        }
        else
        {
            this.gameObject.transform.Translate(-0.01f * beltConveyor.speed, 0, 0);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Limit"))
        {
            Destroy(this.gameObject);
        }
    }
    
}
