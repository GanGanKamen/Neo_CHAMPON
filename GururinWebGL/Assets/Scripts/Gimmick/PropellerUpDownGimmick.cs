using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerUpDownGimmick : MonoBehaviour
{

    private float _speed;
    public bool stop;

    [SerializeField] PropellerGimmick propellerGimmick;
    [SerializeField] PropellerRotationGear propellerRotationGear;

    // Start is called before the first frame update
    void Start()
    {
        _speed = propellerRotationGear.rotSpeed / 85.0f;
        stop = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Max"))
        {
            stop = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //プロペラに風が当たっているときに動く
        if (!stop && propellerGimmick.hitWind)
        {
            //rd2d.velocity = Vector3.up * Time.deltaTime * speed;
            this.gameObject.transform.Translate(0.0f, _speed, 0.0f);
        }
        else if (_speed < 0)
        {
            gameObject.transform.Translate(0.0f, 0.0f, 0.0f);
        }
    }
}
