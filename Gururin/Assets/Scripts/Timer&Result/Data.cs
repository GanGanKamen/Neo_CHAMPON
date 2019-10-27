using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{

    public float[] scenetime;
    public int checkcount = 0;
    public bool destroy = false;

    // Start is called before the first frame update
    void Start()
    {
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;
        

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(destroy)
        {
            SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
        }
    }



    //第一引数(遷移後のシーン),第二引数(シーンの読み込みモード(Single or Additive))
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        if (nextScene.name == "Result")
        {

            SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
            //Debug.Log(scenetime[checkcount]);
            Debug.Log("b");
            
        }
        else if(nextScene.name == "Title")
        {
            //SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());

            //sceneChange = GameObject.Find("SceneChange").GetComponent<SceneChange>();
            Destroy(this.gameObject);

            Debug.Log("c");
        }
        /*else
        {
            gameClear = GameObject.Find("NextStage").GetComponent<GameClear>();
            sceneChange = GameObject.Find("NextStage").GetComponent<SceneChange>();
            
            timer.conversationController = GameObject.Find("ConversationController").GetComponent<ConversationController>();
            timer.flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
            timer.gameClear = GameObject.Find("NextStage").GetComponent<GameClear>();

            timer.scenetime = 0;

            Debug.Log("a");
        }*/
    }
}
