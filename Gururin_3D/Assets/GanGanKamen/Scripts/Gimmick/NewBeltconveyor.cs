using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class NewBeltconveyor : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed;
        [SerializeField] private GameObject conveyorObject;
        [SerializeField] [Range(-1,1)]private int direction;
        private Material beltMaterial;
        private float scrollCount = 0;
        private GameController gameController;
        // Start is called before the first frame update
        void Start()
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            beltMaterial = conveyorObject.GetComponent<MeshRenderer>().material;
            beltMaterial.SetTextureOffset("_MainTex", new Vector2(scrollCount,0));
            var conveyorScale = conveyorObject.transform.localScale;
            gameObject.AddComponent<BoxCollider>();
            gameObject.GetComponent<BoxCollider>().size =
                new Vector3(conveyorScale.x, 1, conveyorScale.z);
            gameObject.GetComponent<BoxCollider>().center = new Vector3(0, -0.5f, 0);
        }

        // Update is called once per frame
        void Update()
        {
            scrollCount += Time.deltaTime * scrollSpeed * direction;
            beltMaterial.SetTextureOffset("_MainTex", new Vector2(scrollCount,0));
        }


        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.gameObject.GetComponent<GururinBase>();
                var playerRb = player.GetComponent<Rigidbody>();
                if(gameController.InputLongPress == false)
                {
                    playerRb.AddForce(scrollSpeed * direction, 0, 0);
                }
                else
                {
                    player.Brake();
                    player.gear.transform.Rotate(0, 0, scrollSpeed * direction * 180f * Time.deltaTime);
                }
            }

        }
    }
}


