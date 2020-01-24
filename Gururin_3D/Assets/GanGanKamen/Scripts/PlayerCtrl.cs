using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : GururinBase
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _gear;
    [SerializeField] private float _jumpPower;

    [SerializeField] private Canvas controllerCanvas;
    [SerializeField] private GameObject gameController;
    private bool isPress = false;
    private Vector3 vecA, vecB;
    // Start is called before the first frame update
    private void Awake()
    {
        speed = _speed;
        gear = _gear;
        jumpPower = _jumpPower;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TestCtrl();
    }

    /*
    private void Operate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 localpoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(controllerCanvas.GetComponent<RectTransform>(),
               Input.mousePosition, controllerCanvas.worldCamera, out localpoint);
            Debug.Log(localpoint);
            Vector2 resultPoint;
            resultPoint = new Vector2(localpoint.x, localpoint.y - 150);

            gameController.GetComponent<RectTransform>().anchoredPosition = resultPoint;
            gameController.SetActive(true);
            isPress = true;

        }

        else if(Input.GetMouseButton(0) && isPress)
        {
            var touchPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        }
    }
    */
    private void TestCtrl()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                base.GururinMove(Input.GetAxis("Horizontal") * 360, true);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                base.GururinMove(-Input.GetAxis("Horizontal") * 360, false);
            }
        }
        else
        {
            base.SpeedReset();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            base.Jump();
        }
    }


}
