using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 足音制御
/// </summary>

public class FootSound : MonoBehaviour
{

    private CriAtomSource _footStep;
    private PlayerMove playerMove;

    // Start is called before the first frame update
    void Start()
    {
        _footStep = GetComponent<CriAtomSource>();
        playerMove = GameObject.Find("Gururin").GetComponent<PlayerMove>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Groundタグと接触したときに足音を鳴らす
        if (other.CompareTag("Ground"))
        {
            _footStep.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove != null)
        {
            if (playerMove.nowGearGimiick!= null)
            {
                GetComponent<PolygonCollider2D>().enabled = false;
            }
            else
            {
                GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }
}
