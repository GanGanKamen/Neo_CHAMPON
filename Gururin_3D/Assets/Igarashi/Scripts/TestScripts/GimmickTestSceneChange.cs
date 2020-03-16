using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GimmickTestSceneChange : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private string loadSceneName;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(() => GameStart());
    }
    private void GameStart()
    {
        SceneManager.LoadScene(loadSceneName);
    }
}
