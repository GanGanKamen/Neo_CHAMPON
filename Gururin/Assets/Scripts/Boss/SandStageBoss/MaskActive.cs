using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskActive : MonoBehaviour
{

    [SerializeField] GameObject _eatMask;
    public bool maskInstance, animEnd;

    // Start is called before the first frame update
    void Start()
    {
        maskInstance = false;
        //Animation上で真偽を切り替え、SandBossMove.csへの継承用
        animEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (maskInstance)
        {
            //マスクをインスタンス
            var mask = Instantiate(_eatMask);
            var pos = transform.position;
            //マスクを所定の位置に移動させる
            //maskPosはまだ仮
            var maskPos = new Vector2(pos.x + 9.75f, pos.y - 2.75f);
            mask.transform.position = new Vector2(maskPos.x, maskPos.y);
            //マスクを削除
            Destroy(mask, 7.0f);
        }
    }
}
