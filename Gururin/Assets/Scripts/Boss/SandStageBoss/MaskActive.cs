using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskActive : MonoBehaviour
{

    [SerializeField] GameObject _mask, _eatMask;
    public bool maskInstance, animEnd;

    // Start is called before the first frame update
    void Start()
    {
        maskInstance = false;
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
            var maskPos = new Vector2(pos.x + 10.5f, pos.y - 2.75f);
            mask.transform.position = new Vector2(maskPos.x, maskPos.y);
            //マスクを削除
            Destroy(mask, 5.0f);
        }
    }
}
