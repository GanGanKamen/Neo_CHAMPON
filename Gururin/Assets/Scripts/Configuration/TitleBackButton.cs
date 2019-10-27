using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackButton : MonoBehaviour
{
    [SerializeField] private Configuration configuration;
    [SerializeField] private UnityEngine.UI.Scrollbar scrollbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        configuration.Method();
        scrollbar.value = 1;
        SoundManager.PlayS(gameObject);
        Fader.FadeIn(2f, "Title");
    }
}
