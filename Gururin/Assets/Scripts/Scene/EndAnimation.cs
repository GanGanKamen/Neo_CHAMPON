using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAnimation : MonoBehaviour
{

    [SerializeField] private Animator _gururinAnim, _hakaseAnim, _textFlashing;
    [SerializeField] CriAtomSource clear, tap;
    public bool[] _sourcePlay;
    private bool _bgmVolume, _backtoTitle;

    // Start is called before the first frame update
    void Start()
    {
        _gururinAnim = GameObject.Find("Gururin_illust").GetComponent<Animator>();
        _hakaseAnim = GameObject.Find("Hakase_illust").GetComponent<Animator>();
        _textFlashing = GameObject.Find("TapTitle").GetComponent<Animator>();

        for (int i = 0; i < 2; i++)
        {
            _sourcePlay[i] = true;
        }
        _bgmVolume = false;
        _backtoTitle = false;

        StartCoroutine("AnimPlay");
    }

    IEnumerator AnimPlay()
    {
        _hakaseAnim.Play("Play");

        yield return new WaitForSeconds(0.5f);

        _gururinAnim.Play("Play");

        yield return new WaitForSeconds(0.2f);

        if (_sourcePlay[0])
        {
            clear.Play();
            _sourcePlay[0] = false;
        }

        yield return new WaitForSeconds(0.2f);

        _backtoTitle = true;
        _textFlashing.Play("Play");

        yield break;
    }

    IEnumerator BacktoTitle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _bgmVolume = true;
            if (_sourcePlay[1])
            {
                tap.Play();
                _sourcePlay[1] = false;
            }

            yield return new WaitForSeconds(1.0f);

            SceneManager.LoadScene("Title");
        }
    }

    private void Update()
    {
        if (_backtoTitle)
        {
            StartCoroutine("BacktoTitle");
        }

        if (_bgmVolume)
        {
            Debug.Log(clear.volume);
            clear.volume -= 0.02f;
            if(clear.volume <= 0.0f)
            {
                clear.volume = 0.0f;
            }
        }
    }
}
