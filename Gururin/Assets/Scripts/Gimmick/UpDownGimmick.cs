using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ラック上げ下げギミック
/// </summary>

public class UpDownGimmick : MonoBehaviour
{

    private float _rotSpeed;
    public bool speed;

    [SerializeField] Gamecontroller gameController;
    [SerializeField] GearGimmick gearGimmick;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();

        if (speed == false)
        {
            _rotSpeed = gearGimmick.rotSpeed / 100.0f;
        }
        else
        {
            _rotSpeed = gearGimmick.rotSpeed / -100.0f;
        }
    }

    //"Max" か "Min"にぶつかったら歯車を停止
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Max")){
            gearGimmick.moveGear[0] = false;
        }
        if (other.CompareTag("Min"))
        {
            gearGimmick.moveGear[0] = false;
            gearGimmick.moveGear[1] = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Max"))
        {
            gearGimmick.moveGear[0] = true;
        }
        if (other.CompareTag("Min"))
        {
            gearGimmick.moveGear[1] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.AxB.z < 0 && gameController.isPress && gearGimmick.playerHit && gearGimmick.moveGear[0])
        {
            this.gameObject.transform.Translate(0.0f, _rotSpeed, 0.0f);
        }
        else if (gameController.AxB.z > 0 && gameController.isPress && gearGimmick.playerHit && gearGimmick.moveGear[1])
        {
            this.gameObject.transform.Translate(0.0f, -_rotSpeed, 0.0f);
        }
    }
}
