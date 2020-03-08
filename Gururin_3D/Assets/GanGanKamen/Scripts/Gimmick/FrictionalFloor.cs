using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class FrictionalFloor : MonoBehaviour
    {
        [SerializeField] [Range(0, 10)] private float frictional;
        private GameController gameController;
        // Start is called before the first frame update
        void Start()
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var gururin = collision.gameObject.GetComponent<PlayerCtrl>();
                if(gururin.IsAccelMove == false && gameController.InputIsPress == false)
                {
                    gururin.Brake(frictional);
                }
            }
        }
    }
}


