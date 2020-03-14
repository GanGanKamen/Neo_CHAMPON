using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    /// <summary>
    /// itemNumber アイテムの番号
    /// 0…アブラ
    /// 1…ゴールドハカセコイン
    /// </summary>
    
    [SerializeField] private int itemNumber;

    private PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GameObject.Find("PlayerStatus").GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch(itemNumber)
            {
                case 0:
                    if(GameObject.FindGameObjectWithTag("System") != null)
                    {
                        var stageMng = GameObject.FindGameObjectWithTag("System").GetComponent<GanGanKamen.StageManager>();
                        stageMng.GetItem();
                    }
                    playerStatus.oil += 1;
                    playerStatus.SpeedUp();
                    playerStatus.Smile();
                    Destroy();
                    break;

                case 1:
                    if (GameObject.FindGameObjectWithTag("System") != null)
                    {
                        var stageMng = GameObject.FindGameObjectWithTag("System").GetComponent<GanGanKamen.StageManager>();
                        stageMng.GetMedal();
                    }
                    playerStatus.coin = true;
                    playerStatus.Smile();
                    Destroy();
                    break;

            }
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
