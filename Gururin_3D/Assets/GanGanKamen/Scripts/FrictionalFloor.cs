using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class FrictionalFloor : MonoBehaviour
    {
        [SerializeField] [Range(0, 10)] private float frictional;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var gururin = collision.gameObject.GetComponent<GururinBase>();
                if(gururin.IsAccelMove == false)
                {
                    gururin.Brake(frictional);
                }
            }
        }
    }
}


