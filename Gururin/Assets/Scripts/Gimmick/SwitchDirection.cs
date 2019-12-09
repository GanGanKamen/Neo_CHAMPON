using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDirection : MonoBehaviour
{

    private GameObject gururin;
    private Rigidbody2D _gururinRb2d;

    private FlagManager _flagManager;
    private Gamecontroller _gameController;
    private PlayerMove _playerMove;

    public enum DirectionType
    {
        None,
        Up, 
        Down
    }
    public DirectionType directionType;

    // Use this for initialization
    void Start()
    {
        _flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        _gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        _playerMove = GameObject.Find("Gururin").GetComponent<PlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _gururinRb2d = other.GetComponent<Rigidbody2D>();

            //移動が反転しているので補正する
            switch (directionType)
            {
                case DirectionType.Up:
                    _flagManager.reverseDirection = false;
                    break;

                case DirectionType.Down:
                    _flagManager.reverseDirection = true;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.flick_up || _gameController.flick_right || _gameController.flick_left)
        {
            
        }
    }
}
