using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRect : MonoBehaviour
{
    public float targetRatio = 16f / 9f;  //理想の比率
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        float currentRatio = Screen.width * 1f / Screen.height;
        float ratio = targetRatio / currentRatio; 

        float RectY = Mathf.Abs(1.0f - ratio) / 2f;

        camera.rect = new Rect(0f, RectY, 1f, ratio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
