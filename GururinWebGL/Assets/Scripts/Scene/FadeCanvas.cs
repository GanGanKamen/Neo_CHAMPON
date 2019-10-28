using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeCanvas : MonoBehaviour
{
    [SerializeField]private Image faderImg;
    private float alpha;
    private int fadeSwitch;
    private float fadeDelta;

    [SerializeField] private GameObject load;
    [SerializeField] private Slider loadSlider;
    [SerializeField] private Image gear;
    private AsyncOperation async;

    private bool _volumeDown;
    private float _volumeBGM, _volumeSE;
    private float bgmDecay, seDecay;

    public string[] bossScenes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        faderImg.color = new Color(faderImg.color.r, faderImg.color.g, faderImg.color.b, alpha);
        switch (fadeSwitch)
        {
            default:
                break;
            case 1:
                if(alpha > 0)
                {
                    alpha -= fadeDelta;
                }
                else
                {
                    alpha = 0;
                    fadeSwitch = 0;
                }
                break;
            case 2:
                if(alpha < 1)
                {
                    alpha += fadeDelta;
                }
                else
                {
                    alpha = 1;
                    fadeSwitch = 0;
                }
                break;
        }
        volumeFade();
    }

    private void volumeFade()
    {
        if (_volumeDown)
        {
            _volumeBGM -= bgmDecay;
            if (_volumeBGM < 0) _volumeBGM = 0;
            _volumeSE -= seDecay;
            if (_volumeSE < 0) _volumeSE = 0;
            CriAtom.SetCategoryVolume("BGM", _volumeBGM);
            CriAtom.SetCategoryVolume("SE", _volumeSE);
        }
    }

    public void FadeOut(float time)
    {
        StartCoroutine(StartFadeOut(time));
    }

    public void FadeIn(float time,string sceneName)
    {
        StartCoroutine(StartFadeIn(time,sceneName));
    }

    private IEnumerator StartFadeOut(float time)
    {
        alpha = 1;
        fadeDelta = Time.deltaTime /time;
        fadeSwitch = 1;
        yield return null;
        while(fadeSwitch != 0)
        {
            yield return null;
        }

    }

    private IEnumerator StartFadeIn(float time,string sceneName)
    {
        alpha = 0;
        fadeDelta = Time.deltaTime / time;

        NeoConfig.isSoundFade = true;
        _volumeBGM = NeoConfig.BGMVolume / 10f;
        _volumeSE = NeoConfig.SEVolume / 10f;
        bgmDecay = (NeoConfig.BGMVolume / 10 * Time.deltaTime) / time;
        seDecay = (NeoConfig.SEVolume / 10 * Time.deltaTime) / time;
        _volumeDown = true;

        fadeSwitch = 2;
        yield return null;
        while (fadeSwitch != 0)
        {
            yield return null;
        }

        load.SetActive(true);
        bool isBoss = false;
        for(int i = 0; i < bossScenes.Length; i++)
        {
            if(bossScenes[i] == sceneName)
            {
                isBoss = true;
            }
        }
        Debug.Log(isBoss);
        if (isBoss) RemainingLife.beforeBossLife = RemainingLife.life;
        async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            loadSlider.value = async.progress;
            gear.rectTransform.Rotate(0, 0, 60f * Time.deltaTime);
            yield return null;
        }
    }
}
