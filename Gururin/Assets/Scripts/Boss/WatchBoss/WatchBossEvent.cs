using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchBossEvent : MonoBehaviour
{
    public int process = 0;
    [SerializeField] private GameObject[] bosses;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BossProcess());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator BossProcess()
    {
        yield return new WaitForSeconds(1f);
        bosses[0].SetActive(true);
        yield break;
    }

    public void nextCamera(int pre,int next)
    {
        bosses[pre].SetActive(false);
        bosses[next].SetActive(true);
        process = next;
    }
}
