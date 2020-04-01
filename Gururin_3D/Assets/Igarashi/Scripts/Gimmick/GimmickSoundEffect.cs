using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickSoundEffect : MonoBehaviour
{
    [SerializeField] private AudioClip SE;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = SE;
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GimmickSE(AudioClip gimmickSE)
    {
        _audioSource.PlayOneShot(gimmickSE);
    }
}
