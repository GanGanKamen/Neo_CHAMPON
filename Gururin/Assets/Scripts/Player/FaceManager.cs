using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ぐるりんの表情制御
/// </summary>

public class FaceManager : MonoBehaviour
{

    public GameObject[] faces;

    private Rigidbody2D _rb2d;
    private FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GameObject.Find("Gururin").GetComponent<Rigidbody2D>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();

        //ゲーム開始時普段顔以外は非表示にしておく
        for(int i = 1; i < faces.Length; i++)
        {
            faces[i].SetActive(false);
        }
    }

    private void Update()
    {
        //顔を回転させないようにする
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void FixedUpdate()
    {
        //歯車と噛み合って回っている時、踏ん張り顔にする
        if (flagManager.standFirm_Face)
        {
            faces[0].SetActive(false);
            faces[1].SetActive(true);
        }
        //ぐるりんのRigidBody.velocity.yが-5以上の時(高いところから落下した時)、びっくり顔にする
        else if (_rb2d.velocity.y < -5.0 || flagManager.surprise_Face)
        {
            faces[0].SetActive(false);
            faces[2].SetActive(true);
        }
        else
        {
            faces[0].SetActive(true);
            for (int i = 1; i < faces.Length; i++)
            {
                faces[i].SetActive(false);
            }
        }
    }
}
