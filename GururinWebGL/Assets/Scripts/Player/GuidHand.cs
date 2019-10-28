using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidHand : MonoBehaviour
{
    public GameObject pressCircle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var posM = Input.mousePosition;
        posM.z = 10f;
        var objPos = Camera.main.ScreenToWorldPoint(posM);
        transform.position = objPos;
        if (Input.GetMouseButton(0))
        {
            pressCircle.SetActive(true);
        }
        else
        {
            pressCircle.SetActive(false);
        }
    }
}
