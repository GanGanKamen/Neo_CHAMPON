using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesMng : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public GameObject ResourcesLoad(string name)
    {
        return Resources.Load<GameObject>("ResourcesMng/" + name);
    }
}
