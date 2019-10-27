using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRotation : MonoBehaviour
{
    private bool _actWind;
    private float _timer;

    public GameObject Wind;

    // Start is called before the first frame update
    void Start()
    {
        _actWind = true;
    }

    // Update is called once per frame
    void Update()
    {
        //3秒間風が出る
        if (_actWind)
        {
            _timer += Time.deltaTime;
            Wind.SetActive(true);

            if (_timer > 3.0f)
            {
                _actWind = false;
                _timer = 0.0f;
            }
        }
        //1秒間風が止まる
        else if (!_actWind)
        {
            _timer += Time.deltaTime;
            StartCoroutine(WindInactCol());

            if (_timer > 1.0f)
            {
                _actWind = true;
                _timer = 0.0f;
            }
        }
        //Debug.Log(_timer);
    }

    IEnumerator WindInactCol()
    {
        Wind.GetComponent<BoxCollider2D>().enabled = false;

        yield return null;

        Wind.SetActive(false);

        yield return new WaitForSeconds(0.3f);

        Wind.GetComponent<BoxCollider2D>().enabled = true;
    }
}
