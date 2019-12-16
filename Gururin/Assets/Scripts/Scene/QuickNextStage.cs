using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickNextStage : MonoBehaviour
{
    public string NextSceneName;
    private bool next = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(next == false)
            {
                next = true;
                SceneManager.LoadScene(NextSceneName);
            }
        }
    }

    public void QuickSceneLoad()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}
