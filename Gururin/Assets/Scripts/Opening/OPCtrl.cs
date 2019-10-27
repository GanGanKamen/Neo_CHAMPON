using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OPCtrl : MonoBehaviour
{
    [SerializeField] private PageCtrl pageCtrl;
    [SerializeField] private OPFadeCtrl fadeCtrl;
    public float waitTime;
    public int category;

    [SerializeField] private string nextSceneName;

    [SerializeField] private Image blackBack;
    private float alpha = 1;
    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFade());
    }

    // Update is called once per frame
    void Update()
    {
        blackBack.color = new Color(blackBack.color.r, blackBack.color.g, blackBack.color.b, alpha);
        if(start)
        {
            if(alpha > 0)
            {
                alpha -= Time.deltaTime / waitTime;
            }
            else
            {
                alpha = 0;
            }
        }
    }

    private IEnumerator StartFade()
    {
        start = true;
        while(alpha != 0)
        {
            yield return null;
        }
        switch (category)
        {
            case 1:
                Page();
                break;
            case 2:
                Fade();
                break;
        }
        yield break;
    }

    private void Page()
    {
        StartCoroutine(PageOperation());
    }

    private IEnumerator PageOperation()
    {
        yield return new WaitForSeconds(waitTime);
        if(pageCtrl.nowPageNum >= pageCtrl.pageNum - 1)
        {
            Fader.FadeInBlack(waitTime, nextSceneName);
            yield break;
        }
        else
        {
            pageCtrl.NextPage(pageCtrl.nowPageNum);
            while (pageCtrl.pageChange != 0)
            {
                yield return null;
            }
            Page();
            yield break;
        }
    }

    private void Fade()
    {
        StartCoroutine(FadeOperation());
    }

    private IEnumerator FadeOperation()
    {
        yield return new WaitForSeconds(waitTime);
        if(fadeCtrl.nowPageNum >= fadeCtrl.pageNum - 1)
        {
            Fader.FadeInBlack(waitTime, nextSceneName);
            yield break;
        }
        else
        {
            fadeCtrl.NextPage(fadeCtrl.nowPageNum);
            while (fadeCtrl.pageChange != 0)
            {
                yield return null;
            }
            Fade();
            yield break;
        }

    }
}
