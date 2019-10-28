using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NeoConfig : MonoBehaviour
{
    static public float BGMVolume;
    static public float SEVolume;
    static public bool isSoundFade = false;
    public Slider BGMSlider,SESlider;
    public GameObject backButton;
    static public float textWaitTime;
    public Slider textSpeedSlider;
    static public bool isToutchToJump;
    public Slider touchSlider;

    private GameObject tapGUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TextSpeed();
        Volume();
        TouchOrFlick();
        if(SceneManager.GetActiveScene().name == "Title")
        {
            backButton.SetActive(false);
        }
        else
        {
            backButton.SetActive(true);
        }

        CtrlUI();
    }

    private void CtrlUI()
    {
        if (Input.GetMouseButtonDown(0) && tapGUI == null)
        {
            tapGUI = Instantiate(ResourcesMng.ResourcesLoad("TAP_1"), Vector3.zero, Quaternion.identity);
        }

        if(tapGUI != null)
        {
            Vector3 tapPosition = Input.mousePosition;
            tapPosition.z = 10f;
            tapGUI.transform.position = Camera.main.ScreenToWorldPoint(tapPosition);
        }

        if(tapGUI != null && Input.GetMouseButtonUp(0))
        {
            Destroy(tapGUI.gameObject);
            tapGUI = null;
        }
    }

    private void Volume()
    {
        if (!isSoundFade)
        {
            BGMVolume = BGMSlider.value;
            SEVolume = SESlider.value;
            CriAtom.SetCategoryVolume("BGM", BGMVolume / 10);
            CriAtom.SetCategoryVolume("Jingle", SEVolume / 10);
            CriAtom.SetCategoryVolume("SE", SEVolume / 10);
            CriAtom.SetCategoryVolume("wind", SEVolume / 10);
        }

    }

    private void TextSpeed()
    {
        if(textSpeedSlider.value == 0)
        {
            textWaitTime = 0.03f;
        }
        else if (textSpeedSlider.value == 1)
        {
            textWaitTime = 0.06f;
        }
        else
        {
            textWaitTime = 0.1f;
        }
    }

    private void TouchOrFlick()
    {
        if(touchSlider.value == 1)
        {
            isToutchToJump = false;
        }
        else
        {
            isToutchToJump = true;
        }
    }
}
