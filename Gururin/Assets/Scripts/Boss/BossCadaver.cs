using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCadaver : MonoBehaviour
{
    public GameObject masterGear;
    private GameObject gururin;
    // Start is called before the first frame update
    void Start()
    {
        gururin = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BreakUp()
    {
        Destroy(masterGear.GetComponent<PolygonCollider2D>());
        masterGear.transform.parent = null;
        masterGear.transform.parent = gururin.transform;
        Destroy(gameObject);
    }
}
