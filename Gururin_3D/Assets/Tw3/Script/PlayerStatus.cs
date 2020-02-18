using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : GanGanKamen.GururinBase
{
    private PlayerCtrl playerCtrl;
    private PlayerFace playerFace;

    public int oil;
    public bool coin;

    [SerializeField] private int facecount;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        playerFace = GameObject.Find("Face").GetComponent<PlayerFace>();
        oil = 0;
        coin = false;

        facecount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(facecount > 0)
        {
            facecount--;
        }
        if(facecount == 0)
        {
            playerFace.Nomal();
        }
    }

    public void SpeedUp()
    {
        maxSpeed = playerCtrl._maxSpeed * (1f + 0.01f * (float)oil);
        accel = playerCtrl._accel * (1f + 0.01f * (float)oil);
    }

    public void Smile()
    {
        facecount = 150;
        playerFace.Smile();
        
    }
    
    public void Face()
    {

    }

}
