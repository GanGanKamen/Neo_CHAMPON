using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAppearance : MonoBehaviour
{
    [SerializeField] [Header("出現させたいオブジェクト")] private GameObject appearanceObject;
    //[SerializeField] [Header("出現位置")] private Transform appearancePos;
    public enum AppearanceType
    {
        Active,
        //Teleportation
    }
    [SerializeField] [Header("出現方法")] private AppearanceType appearanceType;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GanGanKamen.PlayerCtrl>())
        {
            switch (appearanceType)
            {
                case AppearanceType.Active:
                    if (appearanceObject.activeSelf == false)
                    {
                        appearanceObject.SetActive(true);
                    }
                    break;

                    /*
                    case AppearanceType.Teleportation:
                        appearanceObject.transform.position = appearancePos.position;

                        Debug.Log("Move");
                        break;
                    */
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
