using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewWind : MonoBehaviour
{
    public int power;
    private float powerCount;
    [SerializeField] private int defuPower;
    [SerializeField] private float powerSpeed;
    [SerializeField] private GameObject windObj;
    [SerializeField] private float windScal;
    [SerializeField] private float windHigh;
    [SerializeField] private AreaEffector2D areaEffector;
    public bool windSwitch;
    [SerializeField] private int switchonLimit;

    [SerializeField] private Transform fanWing;
    [SerializeField] private float fanWingSpeed;

    [SerializeField] private Slider rotationSlider;
    // Start is called before the first frame update
    void Start()
    {
        powerCount = defuPower;
        power = defuPower;
        rotationSlider.maxValue = switchonLimit - defuPower;
    }

    // Update is called once per frame
    void Update()
    {
        PowerChange();
    }

    public void PowerUp()
    {
        powerCount += Time.deltaTime * powerSpeed;
        if (windSwitch)
        {
            windObj.transform.localPosition += new Vector3(windHigh * Time.deltaTime * powerSpeed, 0, 0);
            windObj.transform.localScale += new Vector3(windScal * Time.deltaTime * powerSpeed, 0, 0);
        }

        Debug.Log("powerup");
    }

    public void PowerDown()
    {
        if (power <= defuPower) return;
        powerCount -= Time.deltaTime * powerSpeed;
        if (windSwitch)
        {
            windObj.transform.localPosition -= new Vector3(windHigh * Time.deltaTime * powerSpeed, 0, 0);
            windObj.transform.localScale -= new Vector3(windScal * Time.deltaTime * powerSpeed, 0, 0);
        }

        Debug.Log("powerdown");
    }

    private void PowerChange()
    {
        rotationSlider.value = power - defuPower;
        power = (int)powerCount;
        //areaEffector.forceMagnitude = power;
        if(windSwitch == false && power >= switchonLimit)
        {
            windSwitch = true;

            windObj.SetActive(true);
        }

        fanWing.Rotate(0, 0, fanWingSpeed * Time.deltaTime * (rotationSlider.value / rotationSlider.maxValue));
    }
}
