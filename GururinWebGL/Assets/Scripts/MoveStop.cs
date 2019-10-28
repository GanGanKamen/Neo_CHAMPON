using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStop : MonoBehaviour
{

    private FlagManager flagManager;
    public bool textFeed;

    // Start is called before the first frame update
    void Start()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            flagManager.velXFixed = true;
            //ぐるりんの動きを止める
            flagManager.moveStop = true;
            //GameControllerを非表示にする
            flagManager.pressParm = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //テキストが送られたら
        if (textFeed)
        {
            flagManager.velXFixed = false;
            //ぐるりんの移動を許可
            flagManager.moveStop = false;
            //GameControllerを表示する
            flagManager.pressParm = true;

            //フラグを使いまわすためにfalseにする
            textFeed = false;

            //このオブジェクトを非表示にする
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject, 0.0f);
        }
    }
}
