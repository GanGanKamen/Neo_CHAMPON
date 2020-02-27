using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titlecontroller : MonoBehaviour
{
    public Titlemove titlemove;
    public GameObject Gururin;
    public int count;
    public bool isCheck;
    public bool isActive;
    [SerializeField] private Animator _textFlashing;
    private bool gamestart = false;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        Gururin.SetActive(false);
        isCheck = false;
        _textFlashing.Play("Play");
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        
        if(count == 10)
        {
            Gururin.SetActive(true);
            isActive = true;
        }
        if(isCheck)
        {
            Gururin.transform.rotation = Quaternion.identity;
            Gururin.SetActive(false);
            isCheck = false;
            count = 0;
        }

    }

    public void GameStartButton()
    {
        if (gamestart) return;
        gamestart = true;
        //SoundManager.PlayS(gameObject);
        Fader.FadeIn(2f, "Tutorial-1");
    }
}
