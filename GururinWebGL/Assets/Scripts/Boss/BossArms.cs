using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class BossArms : MonoBehaviour
    {
        public GameObject armparts;
        [SerializeField] private BossHand bossHand;
        [SerializeField] private int size;
        [SerializeField] private GameObject[] allArmparts;
        // Start is called before the first frame update
        void Start()
        {
            allArmparts = new GameObject[size];
            for (int i = 0; i < size; i++)
            {
                allArmparts[i] = Instantiate(armparts, transform.position, Quaternion.identity);
                allArmparts[i].transform.parent = transform;
            }
        }

        // Update is called once per frame
        void Update()
        {
            var intervalX = (bossHand.transform.position.x - transform.position.x) / size;
            var intervalY = (bossHand.transform.position.y - transform.position.y) / size;
            allArmparts[0].transform.position = bossHand.transform.position
                - new Vector3(intervalX,intervalY,0);
            var vec0 = (bossHand.transform.position - allArmparts[0].transform.position).normalized;
            allArmparts[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, vec0);
            for (int i = 1; i < size-1; i++)
            {
                allArmparts[i].transform.position = allArmparts[i - 1].transform.position
                     - new Vector3(intervalX, intervalY, 0);
                var vec = (allArmparts[i - 1].transform.position - allArmparts[i].transform.position).normalized;
                allArmparts[i].transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
            }
            allArmparts[size-1].transform.position = transform.position;
        }
    }
}

