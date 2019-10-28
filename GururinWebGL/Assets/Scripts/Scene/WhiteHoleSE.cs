using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHoleSE : MonoBehaviour
{

    private CriAtomSource _source;
    private GameObject _gururin;
    private float _distance;
    //[SerializeField] ConversationController conversationController;
    //AnimatorStateInfo stateInfo;
    //private bool _volume;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<CriAtomSource>();
        _gururin = GameObject.Find("Gururin");
        _source.volume = 0.0f;
        _source.Play();
        //_volume = false;
    }


    private void Update()
    {
        var gururinPos = _gururin.transform.position;

        //ぐるりんと扉の距離を取得
        _distance = Vector3.Distance(gururinPos, transform.position);
        //Debug.Log(_distance);

        //距離に応じて音量を変化
        if (6.0f >= _distance)
        {
            _source.volume = 0.5f;
        }
        else if (7.0f >= _distance)
        {
            _source.volume = 0.4f;
        }
        else if (8.0f >= _distance)
        {
            _source.volume = 0.3f;
        }
        else if (9.0f >= _distance)
        {
            _source.volume = 0.2f;
        }
        else if (10.0f >= _distance)
        {
            _source.volume = 0.1f;
        }
        else
        {
            _source.volume = 0.0f;
        }
    }
}
