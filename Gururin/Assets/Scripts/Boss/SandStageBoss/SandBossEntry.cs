using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBossEntry : MonoBehaviour
{
    [SerializeField] private GameObject _bossChase;
    public bool impulsePlay, animEnd;

    private FlagManager _flagManager;
    private SandBossMove _sandBossMove;
    private MaskActive _maskActive;

    [SerializeField] private Animator _entryAnim;

    private Cinemachine.CinemachineImpulseSource _impulse;

    // Start is called before the first frame update
    void Start()
    {
        _flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        _sandBossMove = GameObject.Find("SandBoss_Anim").GetComponent<SandBossMove>();
        _maskActive = transform.Find("SandBoss_Anim/BossMouth").GetComponent<MaskActive>();

        _impulse = GetComponent<Cinemachine.CinemachineImpulseSource>();
        _impulse.m_ImpulseDefinition.m_AmplitudeGain = 0.0f;

        _entryAnim.Play("Idle");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _flagManager.moveStop = true;
            _entryAnim.Play("SandBoss_Entry");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (impulsePlay)
        {
            case true:
                _impulse.m_ImpulseDefinition.m_AmplitudeGain = 0.5f;
                _impulse.GenerateImpulse();
                _maskActive.maskInstance = true;
                break;

            case false:
                _maskActive.maskInstance = false;
                break;
        }

        if (animEnd)
        {
            _bossChase.SetActive(true);
            _flagManager.moveStop = false;
            gameObject.SetActive(false);
        }
    }
}
