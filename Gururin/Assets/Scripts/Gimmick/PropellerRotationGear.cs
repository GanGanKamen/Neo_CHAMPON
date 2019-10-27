using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRotationGear : MonoBehaviour
{

    public float rotSpeed;
    //private Rigidbody2D rb2d;

    [SerializeField] PropellerGimmick propellerGimmick;
    [SerializeField] PropellerUpDownGimmick propellerUpDownGimmick;
    
    // Start is called before the first frame update
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (propellerGimmick.hitWind && !propellerUpDownGimmick.stop)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, -rotSpeed));
        }
        else if (propellerUpDownGimmick.stop)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
        }
    }
}
