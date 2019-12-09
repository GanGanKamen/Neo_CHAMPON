using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ラック張り付き時にぐるりんの位置を固定
/// </summary>

public class TransformFixed : MonoBehaviour
{

    private CriAtomSource _gearMesh;
    public GameObject gearPos;

    private Rigidbody2D rb2d;

    private FlagManager flagManager;
    [SerializeField] private GearRackController _gearRackController;

    //ぐるりんの移動を固定させるタイプの指定
    public enum FixedType
    {
        None,
        Nomal,
        RackGear
    }
    public FixedType fixedType;

    // Start is called before the first frame update
    void Start()
    {
        _gearMesh = GetComponent<CriAtomSource>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        gearPos.SetActive(false);

        //FixedTypeを選択しなかった場合に忠告文を出す
        if (fixedType == FixedType.None)
        {
            Debug.LogError("FixedTypeを選択してください");
        }
        else if(fixedType == FixedType.RackGear)
        {
            _gearRackController = GameObject.Find("GearRackController").GetComponent<GearRackController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (fixedType)
            {
                case FixedType.Nomal:
                    Fixed(other);
                    break;

                case FixedType.RackGear: 
                    StartCoroutine(GearRack(other));
                    other.transform.parent = GameObject.Find("RotateGear").transform;
                    break;
            }
        }
    }

    private void Fixed(Collider2D guruCol)
    {
        //効果音
        _gearMesh.Play();
        //ぐるりんのRigidBody2Dを取得
        var _gururinRb2d = guruCol.GetComponent<Rigidbody2D>();
        rb2d = _gururinRb2d;
        //ぐるりんの位置を固定
        _gururinRb2d.velocity = Vector2.zero;
        _gururinRb2d.position = gearPos.transform.position;
        //ぐるりんの角度を固定
        _gururinRb2d.rotation = gearPos.transform.rotation.eulerAngles.z;
        //ぐるりんの動きを止める
        flagManager.moveStop = true;
        flagManager.VGcol = true;
    }

    IEnumerator GearRack(Collider2D guruCol)
    {
        Fixed(guruCol);

        yield return null;

        flagManager.moveStop = false;
        flagManager.VGcol = false;
        rb2d.gravityScale = 0.0f;

        yield return null;

        /*
        GameObject[] traFixeds = GameObject.FindGameObjectsWithTag("TransformFixed");
        foreach (GameObject traFixed in traFixeds)
        {
            //traFixed.GetComponent<CapsuleCollider2D>().enabled = false;
            traFixed.SetActive(false);
        }
        */
        _gearRackController.traFixedActive = true;

        yield break;
    }
}
