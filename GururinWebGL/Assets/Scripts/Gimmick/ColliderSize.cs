using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 上ラックとくっついたとき
/// </summary>

public class ColliderSize : MonoBehaviour
{

    private BoxCollider2D _boxCol;
    private float _orgSizeX;

    // Start is called before the first frame update
    void Start()
    {
      _boxCol = GetComponent<BoxCollider2D>();
        _orgSizeX = _boxCol.size.x;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _boxCol.size = new Vector2(1.5f, _boxCol.size.y);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _boxCol.size = new Vector2(_orgSizeX, _boxCol.size.y);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
