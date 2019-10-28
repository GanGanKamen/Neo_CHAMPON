using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 扇風機の羽の回転
/// </summary>


public class FanWingRotation : MonoBehaviour
{

    private float rotSpeed;
    private bool _visible;
    public bool windAct;

    [SerializeField] RotationCounter _rotationCounter;

    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 0.0f;
        _visible = false;
    }

    //カメラ(シーンビューも含めて)に映っているとき
    private void OnBecameVisible()
    {
        _visible = true;
    }

    //カメラ(シーンビューも含めて)に映っていないとき
    private void OnBecameInvisible()
    {
        _visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //カメラに映っていないときは動作停止
        if (_visible)
        {
            //RotationCounterのcount数によって羽の回転速度を変動
            if (_rotationCounter != null)
            {
                switch (_rotationCounter.count)
                {
                    case 0:
                        rotSpeed = 0.0f;
                        break;

                    case 1:
                        rotSpeed = 3.0f;
                        break;

                    case 2:
                        rotSpeed = 6.0f;
                        break;

                    case 3:
                        rotSpeed = 9.0f;
                        break;

                    case 4:
                        rotSpeed = 12.0f;
                        break;

                    case 5:
                        rotSpeed = 15.0f;
                        break;
                }
            }

            if (windAct)
            {
                rotSpeed = 15.0f;
            }

            transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed));
        }
    }
}
