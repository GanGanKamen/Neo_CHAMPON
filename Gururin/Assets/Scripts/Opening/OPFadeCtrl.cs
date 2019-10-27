using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OPFadeCtrl : MonoBehaviour
{
    [SerializeField] private RawImage[] pages;
    public int pageNum;
    private float[] alphas;
    [SerializeField] private float speed;
    [Range(0, 2)] public int pageChange;

    public int nowPageNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        pageNum = pages.Length;
        alphas = new float[pageNum];
        for (int i = 0; i < pageNum; i++)
        {
            alphas[i] = 1f;
            pages[i].color = new Color(1, 1, 1, alphas[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pageNum; i++)
        {
            pages[i].color = new Color(1, 1, 1, alphas[i]);
        }

        switch (pageChange)
        {
            default:
                break;
            case 1:
                if(alphas[nowPageNum] > 0)
                {
                    alphas[nowPageNum] -= speed * Time.deltaTime;
                }
                else
                {
                    alphas[nowPageNum] = 0;
                    nowPageNum += 1;
                    pageChange = 0;
                }
                break;
            case 2:
                if (alphas[nowPageNum-1] < 1f)
                {
                    alphas[nowPageNum-1] += speed * Time.deltaTime;
                }
                else
                {
                    alphas[nowPageNum-1] = 1f;
                    nowPageNum -= 1;
                    pageChange = 0;
                }
                break;
        }
    }

    public void NextPage(int nowPage)
    {
        if (nowPage >= pageNum - 1) return;
        pageChange = 1;
    }

    public void PrevPage(int nowPage)
    {
        if (nowPage < 1) return;
        pageChange = 2;
    }

    private void KeyTest()
    {
        if (pageChange != 0) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            PrevPage(nowPageNum);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            NextPage(nowPageNum);
        }
    }
}
