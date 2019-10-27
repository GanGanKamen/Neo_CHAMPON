using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChara : MonoBehaviour
{
    public bool hasDown;
    private Animator animator;

    public Sprite[] eyes;
    [SerializeField] private GameObject ballon;
    [SerializeField] private SpriteRenderer eye;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasDown = true;
            ballon.SetActive(false);
            eye.sprite = eyes[1];
        }
    }

    public void Recovery()
    {
        animator.SetTrigger("Battle");
        ballon.SetActive(true);
        eye.sprite = eyes[0];
    }
}
