using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    public int hp;
    [SerializeField] private Transform mask;
    private float maskPosY;
    [SerializeField] private float maskTranslateSpeed;
    [SerializeField] private float magnification; //hpとmask position.Yの倍率
    private bool isBreak = false;
    // Start is called before the first frame update
    void Start()
    {
        maskPosY = mask.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        MaskTranslate();
        //BreakUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hp <= 0) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            hp -= (int)rigidbody2D.velocity.magnitude;
            maskPosY += (int)rigidbody2D.velocity.magnitude * magnification;
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
