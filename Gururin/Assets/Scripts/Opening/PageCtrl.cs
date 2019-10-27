using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageCtrl : MonoBehaviour
{
    [SerializeField] private RawImage[] pages;
    public int pageNum;
    private Material[] pageMaterials;
    private float[] flips;
    [SerializeField] private float speed;
    [Range(0, 2)] public int pageChange;

    public int nowPageNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        pageNum = pages.Length;
        pageMaterials = new Material[pageNum];
        flips = new float[pageNum];
        for(int i= 0;i< pageNum; i++)
        {
            pageMaterials[i] = pages[i].material;
            flips[i] = 1;
            pageMaterials[i].SetFloat("_Flip", flips[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < pageNum; i++)
        {
            pageMaterials[i].SetFloat("_Flip", flips[i]);
        }

        switch (pageChange)
        {
            default:
                break;
            case 1:
                if(flips[nowPageNum] > -1f)
                {
                    flips[nowPageNum] -= speed * Time.deltaTime;
                }
                else
                {
                    flips[nowPageNum] = -1f;
                    nowPageNum += 1;
                    pageChange = 0;
                }
                break;
            case 2:
                if (flips[nowPageNum-1] <1f)
                {
                    flips[nowPageNum-1] += speed * Time.deltaTime;
                }
                else
                {
                    flips[nowPageNum-1] = 1f;
                    nowPageNum -= 1;
                    pageChange = 0;
                }
                break;
        }
        KeyTest();
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
