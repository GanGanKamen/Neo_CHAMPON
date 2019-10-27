using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackCollider : MonoBehaviour
{

    private FlagManager flagManager;
    private BoxCollider2D _rackCol;

    // Start is called before the first frame update
    void Start()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        _rackCol = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            //TransformFixedのタグを取得、タグを持つオブジェクトを非表示にする
            GameObject[] traFixeds = GameObject.FindGameObjectsWithTag("TransformFixed");

            foreach (GameObject traFixed in traFixeds)
            {
                traFixed.GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Regene", 0.3f);
        }
    }

    void Regene()
    {
        GameObject[] traFixeds = GameObject.FindGameObjectsWithTag("TransformFixed");

        foreach (GameObject traFixed in traFixeds)
        {
            traFixed.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flagManager.VGcol)
        {
            _rackCol.enabled = true;
        }
    }
}
