using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerGearGimmick : MonoBehaviour
{

    public GameObject gear; //カウンターとの接触判定を取る歯車
    public GameObject gearPos;  //ぐるりんの位置
    public bool playerHit;  //ぐるりんとの接触判定
    public bool[] moveGear; //歯車とぐるりんの回転方向
    public bool jumpDirection; //ぐるりんが歯車から離れるときのジャンプ方向、True:左方向 False:右方向

    public bool click;
    public bool rotFlag; //歯車の回転方向の固定、trueなら左回転、falseなら右回転
    private float rotSpeed = 3.0f; //歯車とぐるりんの回転速度
    private static CriAtomSource source; //効果音

    private Rigidbody2D _gururinRb2d; //ぐるりんのRigidbody
    private Quaternion _gpQuaternion; //GururinPosの角度

    private PlayerMove playerMove;
    private Gamecontroller gameController;
    private FlagManager flagManager;
    [SerializeField] RotationCounter rotationCounter;

    // Start is called before the first frame update
    void Start()
    {
        source =GetComponent<CriAtomSource>();
        playerMove = GameObject.Find("Gururin").GetComponent<PlayerMove>();
        gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        _gururinRb2d = GameObject.Find("Gururin").GetComponent<Rigidbody2D>();

        _gpQuaternion = gearPos.transform.rotation;

        moveGear[0] = true;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //FlagManagerのほうのジャンプ方向を上書き、PlayerMove側に伝える
            flagManager.gururinJumpDirection = jumpDirection;

            //ぐるりんとGearの接触を感知
            playerHit = true;
            //playerMove.gearGimmickHit = true;

            //Playerの回転を許可
            click = true;

            //速度の上書きを止める
            playerMove.setSpeed = false;
            //回転速度を固定
            playerMove.speed[0] = rotSpeed;
            //ぐるりんの移動を止める
            playerMove.isMove = false;

            //効果音を鳴らす
            source.Play();

            _gururinRb2d.velocity = Vector2.zero;

            _gururinRb2d.isKinematic = true;
            //ぐるりんの位置を固定
            _gururinRb2d.MovePosition(gearPos.transform.position);
            //ぐるりんの角度を固定
            _gururinRb2d.rotation = _gpQuaternion.eulerAngles.z;


            flagManager.moveStop = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && click)
        {
            _gururinRb2d.isKinematic = true;
            //ぐるりんの位置を固定
            _gururinRb2d.MovePosition(gearPos.transform.position);
            //ぐるりんの角度を固定
            _gururinRb2d.rotation = _gpQuaternion.eulerAngles.z;

            
            flagManager.moveStop = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHit = false;
            playerMove.isMove = true;
            playerMove.setSpeed = true;
            flagManager.moveStop = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //左回転
        if (gameController.AxB.z < 0 && gameController.isPress && playerHit && moveGear[0] && rotFlag)
        {
            click = false;
            flagManager.standFirm_Face = true;
            gear.transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed));
            _gururinRb2d.rotation += -rotSpeed;
        }
        //右回転
        else if (gameController.AxB.z > 0 && gameController.isPress && playerHit && moveGear[0] && !rotFlag)
        {
            click = false;
            flagManager.standFirm_Face = true;
            gear.transform.Rotate(new Vector3(0.0f, 0.0f, -rotSpeed));
            _gururinRb2d.rotation += rotSpeed;
        }
        else if(gameController.isPress == false)
        {
            flagManager.standFirm_Face = false;
        }
    }

    //isPressが押された後(Update後)に判定
    private void LateUpdate()
    {
        if (playerMove.isPress && playerHit)
        {
            _gururinRb2d.isKinematic = false;
            StartCoroutine(GearCollider());
        }
    }

    IEnumerator GearCollider()
    {
        //歯車のColliderを非表示
        GetComponent<CapsuleCollider2D>().enabled = false;

        yield return new WaitForSeconds(0.3f);

        //歯車のColliderを表示
        GetComponent<CapsuleCollider2D>().enabled = true;

        yield break;
    }
}