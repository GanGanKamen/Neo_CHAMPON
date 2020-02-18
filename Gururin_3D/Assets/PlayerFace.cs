using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFace : MonoBehaviour
{
    public GameObject[] faces;

    // Start is called before the first frame update
    void Start()
    {
        //ゲーム開始時普段顔以外は非表示にしておく
        for (int i = 1; i < faces.Length; i++)
        {
            faces[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //顔を回転させないようにする
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void Nomal()
    {
        faces[0].SetActive(true);
        for (int i = 1; i < faces.Length; i++)
        {
            faces[i].SetActive(false);
        }
    }

    public void Smile()
    {
        faces[0].SetActive(false);
        faces[1].SetActive(true);
    }

    public void Angry()
    {
        faces[0].SetActive(false);
        faces[2].SetActive(true);
    }
}
