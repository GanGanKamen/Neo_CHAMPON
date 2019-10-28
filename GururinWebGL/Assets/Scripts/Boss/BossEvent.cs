using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class BossEvent : MonoBehaviour
{
    public Gamecontroller gamecontroller;

    [SerializeField] private RectTransform topBand, bottomBand;
    [SerializeField] private Vector2 topStart, topOver;
    [SerializeField] private Vector2 bottomStart, bottomOver;

    private Vector2 topDis, bottomDis;
    private int cutin = 0;

    [SerializeField] private BossChara bossChara;
    [SerializeField] private GameObject boss;
    [SerializeField] private RectTransform window;
    [SerializeField] private Animator windowAnim;
    [SerializeField] private Text windowText;
    [SerializeField] private GanGanKamen.BossBalloon bossBalloon;

    private PlayerMove player;

    [SerializeField] private Image fader;
    private bool fadeOut = false;
    private float fadeAlpha = 1;

    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;

    public Slider finish;

    public BossCadaver bossCadaver;
    [SerializeField] GameObject nextStage;
    public bool goBack;
    [SerializeField] GameObject lines;

    public GameObject bgmObj;

    public BoxCollider2D[] deadZones;

    IEnumerator canSkipEvent;

    [SerializeField] private float cutInSpeed;
    public Animator[] fanAnims;
    public GameObject[] fanRotations;
    private SimpleConversation conversation;

    public SpriteRenderer lifeRender;
    public Sprite newLife;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        topBand.localPosition = topStart;
        bottomBand.localPosition = bottomStart;
        topDis = topStart;
        bottomDis = bottomStart;
        fadeAlpha = 1;
        canSkipEvent = BossStart();
        conversation = GetComponent<SimpleConversation>();
    }

    void Start()
    {
        if (RemainingLife.life == RemainingLife.beforeBossLife) StartCoroutine(canSkipEvent);
        else StartCoroutine(SkipFade());

        StartCoroutine(BossClear());
    }

    // Update is called once per frame
    void Update()
    {
        if (cutin == 1) //movieCutOut
        {
            if (topBand.localPosition.y < topDis.y)
            {
                topBand.localPosition += new Vector3(0, Time.deltaTime * cutInSpeed, 0);
            }
            else
            {
                cutin = 0;
            }
            if (bottomBand.localPosition.y > bottomDis.y)
            {
                bottomBand.localPosition -= new Vector3(0, Time.deltaTime * cutInSpeed, 0);
            }
            else
            {
                cutin = 0;
            }
        }
        else if(cutin == 2)//movieCutIn
        {

            if (topBand.localPosition.y > topDis.y)
            {
                topBand.localPosition -= new Vector3(0, Time.deltaTime * cutInSpeed, 0);
            }
            else
            {
                cutin = 0;
            }
            if (bottomBand.localPosition.y < bottomDis.y)
            {
                bottomBand.localPosition += new Vector3(0, Time.deltaTime * cutInSpeed, 0);
            }
            else
            {
                cutin = 0;
            }
            //topBand.localPosition = Vector2.Lerp(topBand.localPosition, topDis, Time.deltaTime);
            //bottomBand.localPosition = Vector2.Lerp(bottomBand.localPosition, bottomDis, Time.deltaTime);
        }
        fader.color = new Color(40f / 255f, 40f / 255f, 40f / 255f, fadeAlpha);
        if (fadeOut)
        {
            fadeAlpha -= Time.deltaTime;
            if (fadeAlpha <= 0)
            {

                fadeOut = false;
            }
        }
        if (goBack)
        {
            player.transform.position += new Vector3(0, Time.deltaTime * 5f, 0);
        }
        if (player.finishMode)
        {
            player.transform.position = new Vector3(2.3f, -0.9f, 0);
        }
    }

    private IEnumerator SkipFade()
    {
        virtualCameras[3].Priority = 1;
        windowAnim.SetBool("Open", false);
        windowAnim.SetBool("Close", true);
        gamecontroller.isCon = false;
        fader.gameObject.SetActive(true);
        fadeOut = true;
        bossBalloon.lifes = RemainingLife.bossLife;
        for (int i = 0; i < fanAnims.Length; i++)
        {
            Destroy(fanAnims[i]);
        }
        while (fadeAlpha > 0)
        {
            yield return null;
        }
        fader.gameObject.SetActive(false);
        fadeAlpha = 1;
        fadeOut = false;
        gamecontroller.isCon = false;//nextFade
        boss.SetActive(true);//nextFade
        SoundManager.PlayS(bgmObj);//nextFade
        yield break;
    }
    /*
    public void SkipButton()
    {
        StartCoroutine(Skip());
    }

    private IEnumerator Skip()
    {
        StopCoroutine(canSkipEvent);
        canSkipEvent = null;
        virtualCameras[3].Priority = 1;
        stop = true;
        bossChara.gameObject.SetActive(false);

        windowAnim.SetBool("Open", false);
        windowAnim.SetBool("Close", true);
        SoundManager.StopS(gameObject);
        MovieCutOut();
        while (Mathf.Abs(topBand.localPosition.y - topDis.y) > 1f)
        {
            yield return null;
        }
        stop = true;
        gamecontroller.isCon = false;//nextFade
        boss.SetActive(true);//nextFade
        SoundManager.PlayS(bgmObj);//nextFade
        yield break;
    }
    */
    private IEnumerator BossStart()
    {
        gamecontroller.isCon = true;
        MovieCutIn();
        yield return null;
        while (cutin != 0)
        {
            yield return null;
        }
        cutin = 0;
        bossChara.gameObject.SetActive(true);
        //SoundManager.PlayS(bossChara.gameObject, "SE_propellerBOSSnakigoe1");
        virtualCameras[3].Priority = 11;
        yield return new WaitForSeconds(5f);
        virtualCameras[3].Priority = 1;
        yield return new WaitForSeconds(2f);
        Vector2 force = new Vector2(0, 300f);
        player.GetComponent<Rigidbody2D>().AddForce(force);
        SoundManager.PlayS(player.gameObject, "SE_jump");
        while (bossChara.hasDown == false)
        {
            yield return null;
        }
        lifeRender.sprite = newLife;
        SoundManager.PlayS(bossChara.gameObject, "SE_propellerBOSSnakigoe2");
        SoundManager.PlayS(bossChara.gameObject, "SE_ballonBreak");
        yield return new WaitForSeconds(3f);
        bossChara.Recovery();
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < fanAnims.Length; i++)
        {
            fanAnims[i].SetBool("Down", true);
        }
        yield return new WaitForSeconds(0.5f);
        conversation.preSentenceNum = -1;
        windowAnim.SetBool("Close", false);
        windowAnim.SetBool("Open", true);

        string finalText = "";
        if (LanguageSwitch.language == LanguageSwitch.Language.Japanese || LanguageSwitch.language == LanguageSwitch.Language.English)
        {
            finalText = conversation.sentences[conversation.currentSentenceNum].TextOutPut().Replace("　", "\n\n");
        }
        else
        {
            finalText = conversation.sentences[conversation.currentSentenceNum].TextOutPut().Replace("　", "\n");
        }

        while (finalText != conversation.Text.text)
        {
            yield return null;
        }
        yield return null;
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        conversation.Text.text = "";
        windowAnim.SetBool("Open", false);
        windowAnim.SetBool("Close", true);
        MovieCutOut();
        yield return null;
        while (cutin != 0)
        {
            yield return null;
        }
        cutin = 0;
        SoundManager.PlayS(bgmObj);
        bossChara.gameObject.SetActive(false);
        boss.SetActive(true);
        gamecontroller.isCon = false;
        for (int i = 0; i < fanAnims.Length; i++)
        {
            fanAnims[i].enabled = false;
        }
        yield break;
    }

    private IEnumerator BossClear()
    {
        while (bossBalloon.lifes > 0)
        {
            yield return null;
        }
        RemainingLife.bossLife = 0;
        for (int i = 0; i < fanRotations.Length; i++)
        {
            SoundManager.StopS(fanRotations[i]);
        }
        for (int i = 0; i < deadZones.Length; i++)
        {
            deadZones[i].enabled = false;
        }
        gamecontroller.isCon = true;
        SoundManager.StopS(bgmObj);

        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(2.5f);
        fader.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        fadeOut = true;
        player.GetComponent<CriAtomSource>().enabled = false;
        player.transform.eulerAngles = new Vector3(0, 0, -90f);
        player.transform.position = new Vector3(3.5f, -1f, 0);
        player.balloon.SetActive(false);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        yield return null;
        while (fadeOut == true)
        {
            yield return null;
        }

        MovieCutIn();
        yield return null;
        while (cutin!=0)
        {
            yield return null;
        }
        cutin = 0;

        player.animator.enabled = true;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        virtualCameras[1].Priority = 11;
        yield return new WaitForSeconds(0.5f);
        player.animator.SetTrigger("Jump");
        /*
        SoundManager.PlayS(gameObject, "allStageGimmick", "SE_jump");
        yield return new WaitForSeconds(1f);
        SoundManager.PlayS(gameObject, "allStageGimmick", "SE_turnGear");
        yield return new WaitForSeconds(1f);
        */
        yield return new WaitForSeconds(2f);
        player.animator.SetTrigger("Kick");
        virtualCameras[1].Priority = 1;
        yield return new WaitForSeconds(1f);
        virtualCameras[2].Priority = 11;
        player.animator.enabled = false;

        window.localPosition = new Vector3(0, -50, 0);
        conversation.currentSentenceNum = 1;
        windowAnim.SetBool("Open", true);
        windowAnim.SetBool("Close", false);
        string finalText = "";
        if (LanguageSwitch.language == LanguageSwitch.Language.Japanese || LanguageSwitch.language == LanguageSwitch.Language.English)
        {
            finalText = conversation.sentences[conversation.currentSentenceNum].TextOutPut().Replace("　", "\n\n");
        }
        else
        {
            finalText = conversation.sentences[conversation.currentSentenceNum].TextOutPut().Replace("　", "\n");
        }

        while (finalText != conversation.Text.text)
        {
            yield return null;
        }
        yield return null;
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        virtualCameras[1].Priority = 11;
        virtualCameras[2].Priority = 1;
        yield return new WaitForSeconds(0.5f);
        gamecontroller.isCon = false;
        player.finishMode = true;
        finish.gameObject.SetActive(true);

        virtualCameras[1].Priority = 11;
        virtualCameras[1].Follow = null;
        virtualCameras[1].LookAt = null;
        virtualCameras[2].Priority = 1;

        lines.SetActive(true);
        while (finish.value < finish.maxValue)
        {
            yield return null;
        }
        nextStage.SetActive(true);
        conversation.Text.text = "";
        windowAnim.SetBool("Open", false);
        windowAnim.SetBool("Close", true);
        //SoundManager.PlayS(gameObject, "Operation", "SE_WindowClose");
        gamecontroller.isCon = true;
        player.finishMode = false;
        finish.gameObject.SetActive(false);
        bossCadaver.BreakUp();
        lines.SetActive(false);
        virtualCameras[1].Priority = 1;
        virtualCameras[2].Priority = 11;
        yield return new WaitForSeconds(0.5f);
        //SoundManager.PlayS(gameObject, "allStageGimmick", "SE_jump");
        goBack = true;
    }

    private void MovieCutIn()
    {
        cutin = 2;
        topDis = topOver;
        bottomDis = bottomOver;
    }

    private void MovieCutOut()
    {
        cutin = 1;
        topDis = topStart;
        bottomDis = bottomStart;
    }
}
