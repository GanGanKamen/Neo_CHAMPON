using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRespawn : MonoBehaviour
{
    [SerializeField] private GameObject _respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCtrl>())
        {
            other.transform.position = _respawnPoint.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
