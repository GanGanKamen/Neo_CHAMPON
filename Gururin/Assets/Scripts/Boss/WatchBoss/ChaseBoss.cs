using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBoss : MonoBehaviour
{
    private PlayerMove player;
    private float count = 0;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float waitTime;
    [SerializeField] private float downTime;
    [SerializeField] private GameObject downObj;
    public bool isDown = false;
    private float confusionCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDown)
        {
            count = 0;
            downObj.SetActive(true);
            if (confusionCount >= downTime)
            {
                isDown = false;
                confusionCount = 0;
            }
            else
            {
                confusionCount += Time.deltaTime;
            }
        }
        else
        {
            downObj.SetActive(false);
            Shoot();
        }

    }

    private void Shoot()
    {
        if(count >= waitTime)
        {
            GameObject attackObj = Instantiate(ResourcesMng.ResourcesLoad("AttackBall"), muzzle.position, Quaternion.identity);
            attackBall attack = attackObj.GetComponent<attackBall>();
            attack.force = (player.transform.position - muzzle.position).normalized;
            count = 0;
        }
        else
        {
            count += Time.deltaTime;
        }
    }
}
