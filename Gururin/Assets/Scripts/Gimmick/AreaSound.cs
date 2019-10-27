using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSound : MonoBehaviour
{

    private bool _sourceStart;
    public float upVolume, maxVolume;
    private CriAtomSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<CriAtomSource>();

        _sourceStart = false;
        _source.volume = 0.0f;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _sourceStart == false)
        {
            _source.Play();
            _sourceStart = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _sourceStart)
        {
            _sourceStart = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_sourceStart)
        {
            _source.volume += upVolume;
            if (_source.volume >= maxVolume)
            {
                _source.volume = maxVolume;
            }
        }
        else if (_sourceStart == false)
        {
            _source.volume -= 0.005f;
            if (_source.volume <= 0.0f)
            {
                _source.volume  = 0.0f;
                _source.Stop();
            }
        }
    }
}
