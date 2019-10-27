using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateLine : MonoBehaviour
{
    private RectTransform rect;
    public float rotSpeed;
    public Gamecontroller gameController;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.AxB.z < 0 && gameController.isPress)
        {
            rect.Rotate(0, 0, rotSpeed);
        }
        else if (gameController.AxB.z > 0 && gameController.isPress)
        {
            rect.Rotate(0, 0, -rotSpeed);
        }
    }
}
