using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private GanGanKamen.GururinBase gururinBase;
    [SerializeField] private PlayerFace playerFace;
    [SerializeField] private float delta;

    public int oil;
    public bool coin;

    [SerializeField] private int facecount;
    
    // Start is called before the first frame update
    void Start()
    {
        oil = 0;
        coin = false;

        facecount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerFace != null)
        {
            if (facecount > 0)
            {
                facecount--;
            }
            if (facecount == 0)
            {
                playerFace.Nomal();
            }
        }

    }

    public void SpeedUp()
    {
        gururinBase.maxSpeed = gururinBase.DefultSpeed * (1f + delta * (float)oil);
        gururinBase.accel = gururinBase.DefultAccel * (1f + delta * (float)oil);
    }

    public void Smile()
    {
        facecount = 150;
        if(playerFace != null) playerFace.Smile();
        
    }
    
    public void Face()
    {

    }

}
