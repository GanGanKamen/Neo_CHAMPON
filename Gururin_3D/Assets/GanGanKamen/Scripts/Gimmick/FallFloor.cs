using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class FallFloor : MonoBehaviour
    {
        public bool IsOnCamera { get { return GetIsOnCamera(); } }

        [SerializeField] private float endurance;
        [SerializeField] private GameObject[] targets;
        [SerializeField] private float shakeSpeed;
        [SerializeField] private float shakeWidth;
        [SerializeField] private float fallSpeed;

        private float[] startPosx;
        private float[] direction;
        private Vector3[] startPos;

        [SerializeField] private float startShakeWaitTime;
        private float startShakeCount = 0;
        [SerializeField] private Collider[] colliders;
        private float recovery;

        private Camera targetCamera;
        private PlayerCtrl playerCtrl;

        public enum Status
        {
            Normal,
            Ready,
            Shake,
            Fall
        }
        public Status status;
        /*
        private void Awake()
        {
            var targetlist = new List<GameObject>();
            foreach(Transform child in transform)
            {
                targetlist.Add(child.gameObject);
            }
            targets = targetlist.ToArray();

            var colliderlist = new List<Collider>();
            foreach(GameObject gameObject in targets)
            {
                colliderlist.Add(gameObject.transform.GetChild(0).GetComponent<Collider>());
            }
            colliders = colliderlist.ToArray();
        }
        */
        // Start is called before the first frame update
        void Start()
        {
            status = Status.Normal;
            startPosx = new float[targets.Length];
            direction = new float[targets.Length];
            startPos = new Vector3[targets.Length];
            for (int i = 0; i < targets.Length; i++)
            {
                startPosx[i] = targets[i].transform.localPosition.x;
                direction[i] = -1;
                startPos[i] = targets[i].transform.position;
            }
            recovery = 0;

            if (GameObject.FindGameObjectWithTag("MainCamera") != null)
            {
                targetCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }
            else
            {
                targetCamera = Camera.main;
            }

            playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
        }

        // Update is called once per frame
        void Update()
        {

            switch (status)
            {
                case Status.Normal:
                    if (IsOnCamera && playerCtrl.CanCtrl)
                    {
                        StartAction();
                    }
                    break;
                case Status.Ready:
                    Ready();
                    break;
                case Status.Shake:
                    Shaking(shakeSpeed, shakeWidth);
                    break;
                case Status.Fall:
                    Falling(fallSpeed);
                    break;
            }

            if (endurance < 0)
            {
                status = Status.Fall;
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].enabled = false;
                }
                endurance = 0;
            }

            if (startShakeCount >= startShakeWaitTime && status == Status.Ready)
            {
                status = Status.Shake;
                startShakeCount = 0;
            }

            if (recovery >= 5f && IsOnCamera == false)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    colliders[i].enabled = true;
                    targets[i].transform.position = startPos[i];
                }
                status = Status.Normal;
                recovery = 0;
            }
        }

        public void StartAction()
        {
            if (status != Status.Normal) return;
            status = Status.Ready;
        }

        private void Shaking(float speed, float width)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (direction[i] == -1)
                {
                    //targets[i].transform.Translate(-speed*Time.deltaTime, 0, 0);
                    targets[i].transform.localPosition -= new Vector3(speed * Time.deltaTime, 0, 0);
                    if (targets[i].transform.localPosition.x <= startPosx[i] - width)
                    {
                        direction[i] = 1;
                    }
                }
                else if (direction[i] == 1)
                {
                    //targets[i].transform.Translate(speed * Time.deltaTime, 0, 0);
                    targets[i].transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
                    if (targets[i].transform.localPosition.x >= startPosx[i] + width)
                    {
                        direction[i] = -1;
                    }
                }
            }

            endurance -= Time.deltaTime;
        }

        private void Ready()
        {
            startShakeCount += Time.deltaTime;
        }

        private void Falling(float speed)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].transform.localPosition -= new Vector3(0, speed * Time.deltaTime, 0);
            }
            recovery += Time.deltaTime;
        }

        private bool GetIsOnCamera()
        {
            var viewportPos = targetCamera.WorldToViewportPoint(transform.position);
            var rect = new Rect(0, 0, 1, 1);
            if (rect.Contains(viewportPos))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
