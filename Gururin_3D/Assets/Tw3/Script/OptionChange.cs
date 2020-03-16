using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionChange : MonoBehaviour
{
    [SerializeField] GameObject optionWindow;
    [SerializeField] GameObject gameOption;
    [SerializeField] GameObject soundOption;
    [SerializeField] GameObject openButton;
    [SerializeField] GameObject closeButton;

    // Start is called before the first frame update
    void Start()
    {
        gameOption.SetActive(true);
        soundOption.SetActive(false);
        optionWindow.SetActive(false);
        openButton.SetActive(true);
        closeButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChangeGame()
    {
        gameOption.SetActive(true);
        soundOption.SetActive(false);
    }

    public void ChangeSound()
    {
        gameOption.SetActive(false);
        soundOption.SetActive(true);
    }

    public void OpenOption()
    {
        optionWindow.SetActive(true);
        closeButton.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void CloseOption()
    {
        optionWindow.SetActive(false);
        openButton.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
