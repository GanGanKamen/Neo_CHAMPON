using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 歯車回転SEの制御
/// </summary>

public class RotationSE : MonoBehaviour
{

    private CriAtomSource _rotSE;
    public bool pan;

    // Start is called before the first frame update
    void Start()
    {
        _rotSE = GetComponent<CriAtomSource>();
        _rotSE.volume = 0.5f;
        if (pan)
        {
            _rotSE.use3dPositioning = true;
            _rotSE.pan3dDistance = 1.0f;
            _rotSE.pan3dAngle = 0.0f;
        }
        else
        {
            _rotSE.use3dPositioning = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gimmick"))
        {
            _rotSE.Play();
        }
    }
}
