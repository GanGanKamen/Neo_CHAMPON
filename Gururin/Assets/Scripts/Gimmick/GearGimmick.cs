using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GearGimmick : MonoBehaviour
{
    public enum Categoly
    {
        normal,
        propeller,
        watch
    }

    public Categoly categoly;

    public GameObject gear; //カウンターとの接触判定を取る歯車
    public GameObject gearPos;  //ぐるりんの位置
    public bool playerHit;  //ぐるりんとの接触判定
    public bool[] moveGear; //歯車とぐるりんの回転方向
    public bool jumpDirection; //ぐるりんが歯車から離れるときのジャンプ方向、True:左方向 False:右方向

    public bool click;
    public bool rotFlag; //歯車の回転方向の固定、trueなら左回転、falseなら右回転
    public bool rotParm;
    public float rotSpeed; //歯車とぐるりんの回転速度
    private static CriAtomSource source; //効果音

    private Rigidbody2D _gururinRb2d; //ぐるりんのRigidbody
    private Quaternion _gpQuaternion; //GururinPosの角度

    private PlayerMove playerMove;
    private Gamecontroller gameController;
    private FlagManager flagManager;

    private GanGanKamen.BossHand bossHand;  //ボスの腕
    private GanGanKamen.BossStageGear stageGear; //ボスステージの歯車

    [SerializeField] BossEvent bossEvent;

    //private PolygonCollider2D gearCol;

    [SerializeField] RotationCounter rotationCounter; //プロペラのスライダー

    public WatchGimick watch; //時計ギミック

    [Header("回転方向")] [Tooltip("1:時計回り 2:反時計回り 3:両方")] [Range(1, 3)] public int turnCategory;
    [SerializeField] private GearTurnUI turnUI;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<CriAtomSource>();
        _gururinRb2d = GameObject.Find("Gururin").GetComponent<Rigidbody2D>();
        playerMove = GameObject.Find("Gururin").GetComponent<PlayerMove>();
        gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();

        _gpQuaternion = gearPos.transform.rotation;

        for (int i = 0; i < moveGear.Length; i++)
        {
            moveGear[i] = true;
        }

        //gearCol = gear.GetComponent<PolygonCollider2D>();

        if (GetComponent<GanGanKamen.BossHand>() != null) bossHand = GetComponent<GanGanKamen.BossHand>();
        if (GetComponent<GanGanKamen.BossStageGear>() != null) stageGear = GetComponent<GanGanKamen.BossStageGear>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMove>().nowBossHand != null) return;
            else if (other.GetComponent<PlayerMove>().nowBossHand == null)
            {
                //FlagManagerのほうのジャンプ方向を上書き、PlayerMove側に伝える
                flagManager.gururinJumpDirection = jumpDirection;
            }
            if (gameController.isPress)
            {
                gameController.isPress = false;
            }
            //ぐるりんとStopColliderの接触を感知
            playerMove.nowGearGimiick = this;
            playerHit = true;

            if (categoly == Categoly.propeller) click = true;
            rotParm = false;

            //速度の上書きを止める
            playerMove.setSpeed = false;
            //回転速度を固定
            playerMove.speed[0] = rotSpeed;
            //ぐるりんの移動を止める
            playerMove.isMove = false;
            _gururinRb2d.velocity = Vector2.zero;
            //効果音
            source.Play();

            _gururinRb2d.velocity = Vector2.zero;

            _gururinRb2d.isKinematic = true;
            //ぐるりんの位置を固定
            _gururinRb2d.MovePosition(gearPos.transform.position);
            //ぐるりんの角度を固定
            _gururinRb2d.rotation = _gpQuaternion.eulerAngles.z;


            flagManager.moveStop = true;

            if (bossHand != null) bossHand.AttachPlayer();

            if (turnUI != null) turnUI.AttachPlayer(turnCategory);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && rotParm == false)
        {
            //ぐるりんの位置を固定
            if (GetComponent<GanGanKamen.BossHand>() == null) _gururinRb2d.MovePosition(gearPos.transform.position);
            //ぐるりんの角度を固定
            _gururinRb2d.rotation = _gpQuaternion.eulerAngles.z;

            flagManager.moveStop = true;

            _gururinRb2d.velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (categoly) //Update()に全部ぶち込んちゃうと見づらいので、categolyごとの処理を関数に格納した
        {
            case Categoly.normal:
                NormalAction();
                break;
            case Categoly.propeller:
                PropellerAction();
                break;
            case Categoly.watch:
                WatchAction();
                break;

        }
    }

    IEnumerator Col()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        //gearCol.enabled = false;

        yield return new WaitForSeconds(0.3f);

        GetComponent<CapsuleCollider2D>().enabled = true;
        //gearCol.enabled = true;
        yield break;
    }

    public void Separation() //ぐるりんが離れる時に他のスクリプトから呼び出す
    {
        StartCoroutine(Col());
        if (bossHand != null) bossHand.Separate();
        _gururinRb2d.isKinematic = false;
        playerHit = false;
        playerMove.isMove = true;
        playerMove.setSpeed = true;
        flagManager.moveStop = false;

        playerMove.nowBossHand = null;
        playerMove.nowGearGimiick = null;
        if (bossHand != null) bossHand.Separate();

        if (turnUI != null) turnUI.SeparatePlayer();
    }

    private void PropellerAction()//プロペラの場合
    {
        if (gameController.AxB.z < 0 && gameController.isPress && playerHit && moveGear[0] && rotFlag)
        {
            //ぐるりんの回転を許可
            rotParm = true;
            click = false;
            flagManager.standFirm_Face = true;
            gear.transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed));
            _gururinRb2d.rotation += -rotSpeed;
        }
        //右回転
        else if (gameController.AxB.z > 0 && gameController.isPress && playerHit && moveGear[0] && !rotFlag)
        {
            //ぐるりんの回転を許可
            rotParm = true;
            click = false;
            flagManager.standFirm_Face = true;
            gear.transform.Rotate(new Vector3(0.0f, 0.0f, -rotSpeed));
            _gururinRb2d.rotation += rotSpeed;
        }
        else if (gameController.isPress == false)
        {
            flagManager.standFirm_Face = false;
        }

    }

    private void NormalAction() //普通の歯車の場合
    {
        if (gameController.AxB.z < 0 && gameController.isPress && playerHit && moveGear[0])
        {
            //ぐるりんの回転を許可
            rotParm = true;
            //ぐるりんの表情を変更
            flagManager.standFirm_Face = true;
            gear.transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed));
            _gururinRb2d.rotation += -rotSpeed;

            if (playerMove.finishMode)//マスターギアを取る
            {
                bossEvent.finish.value += Time.deltaTime;
            }
        }
        else if (gameController.AxB.z > 0 && gameController.isPress && playerHit && moveGear[1])
        {
            //ぐるりんの回転を許可
            rotParm = true;
            flagManager.standFirm_Face = true;
            gear.transform.Rotate(new Vector3(0.0f, 0.0f, -rotSpeed));
            _gururinRb2d.rotation += rotSpeed;
            if (stageGear != null)  //ボスステージの歯車を回す
            {
                if (stageGear.direction == -1)
                {
                    stageGear.GearTurn(true, true);
                }
                else
                {
                    stageGear.GearTurn(false, true);
                }
            }
            if (playerMove.finishMode) //マスターギアを取る
            {
                bossEvent.finish.value += Time.deltaTime;
            }
        }
        //回転操作をしていないときは普通の顔にする
        else if (gameController.isPress == false)
        {
            flagManager.standFirm_Face = false;
        }
    }

    private void WatchAction() //時計の歯車の場合
    {
        if (gameController.AxB.z < 0 && gameController.isPress && playerHit && moveGear[0])
        {
            //ぐるりんの回転を許可
            rotParm = true;
            //ぐるりんの表情を変更
            flagManager.standFirm_Face = true;
            gear.transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed));
            _gururinRb2d.rotation += -rotSpeed;

            WatchGearTurn(true);

            if (playerMove.finishMode)//マスターギアを取る
            {
                bossEvent.finish.value += Time.deltaTime;
            }
        }
        else if (gameController.AxB.z > 0 && gameController.isPress && playerHit && moveGear[1])
        {
            //ぐるりんの回転を許可
            rotParm = true;
            flagManager.standFirm_Face = true;
            gear.transform.Rotate(new Vector3(0.0f, 0.0f, -rotSpeed));
            _gururinRb2d.rotation += rotSpeed;

            WatchGearTurn(false);
        }
        //回転操作をしていないときは普通の顔にする
        else if (gameController.isPress == false)
        {
            flagManager.standFirm_Face = false;
        }
    }

    private void WatchGearTurn(bool direction)
    {
        if (direction == true) watch.PointerRotate(true);
        else
        {
            watch.PointerRotate(false);
        }
    }
}
