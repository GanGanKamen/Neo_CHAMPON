using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    public int hp;
    private Rigidbody2D _gururinRb2D;
    [SerializeField] private Transform mask;
    private float maskPosY;
    [SerializeField] private float maskTranslateSpeed;
    [SerializeField] private float magnification; //hpとmask position.Yの倍率
    private bool isCollision = false;
    private float beforeGururinSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _gururinRb2D = GameObject.Find("Gururin").GetComponent<Rigidbody2D>();
        maskPosY = mask.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //毎Fぐるりんの速度を取得
        float nowGururinSpeed = _gururinRb2D.velocity.magnitude;
        //BreakWallと衝突していないときにbeforeGururinSpeedを更新 = 衝突したときは前のFの速度を取得する
        if (isCollision == false) {
            beforeGururinSpeed = nowGururinSpeed;
        }

        MaskTranslate();
        //BreakUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hp <= 0) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollision = true;
            //Rigidbody2D rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            //hp -= (int)rigidbody2D.velocity.magnitude;
            hp -= (int)beforeGururinSpeed;
            Debug.Log("HP : " + hp);
            Debug.Log("CollisionVelocity : " + beforeGururinSpeed);
            maskPosY += (int)beforeGururinSpeed * magnification;
        }
    }

    private void MaskTranslate()
    {
        // hpが0以下なら即壁を破壊
        if(hp <= 0)
        {
            StartCoroutine(BreakUp());
        }
        else if(hp >= 0 && maskPosY > mask.transform.position.y)
        {
            isCollision = false;
            mask.localPosition += new Vector3(0, maskTranslateSpeed * Time.deltaTime, 0);
        }
    }

    /*
    private void BreakUp()
    {
        if(hp <= 0 && maskPosY <= mask.transform.position.y)
        {
            gameObject.SetActive(false);
        }
    }
    */

    IEnumerator BreakUp()
    {
        mask.localPosition += new Vector3(0, maskTranslateSpeed * 5.0f * Time.deltaTime, 0);

        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);

        yield break;
    }
}
