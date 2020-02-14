using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] private GameObject creditObj;
    // Start is called before the first frame update
    private void Awake()
    {
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (creditObj != null) CloseCredit();
    }

    public void CreditButton()
    {
        if (creditObj == null) return;
        creditObj.SetActive(true);
        SoundManager.PlayS(gameObject, "SE_WindowOpen");
    }

    private void CloseCredit()
    {
        if (creditObj.activeSelf && Input.GetMouseButtonDown(0))
        {
            creditObj.SetActive(false);
            SoundManager.PlayS(gameObject, "SE_WindowOpen");
        }
    }
}
