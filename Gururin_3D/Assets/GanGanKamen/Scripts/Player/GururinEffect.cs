using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class GururinEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem footSmoke;
        [SerializeField] private float footSmokeLimitSpeed;
        [SerializeField] private GameObject transAmGururin;
        [SerializeField] private float transAmLimitSpeed;
        [SerializeField] private int transAmOutput;
        [SerializeField] private int transAmDensity;
        private GururinBase gururin;
        private List<SkinnedMeshRenderer> gururinClones;
        private int transAmCount;
        private Vector3 prePosition;
        private Vector3 positionDelta;
        private Vector3 preAngle;
        private Vector3 angleDelta;
        // Start is called before the first frame update
        void Start()
        {
            gururin = GetComponent<GururinBase>();
            gururinClones = new List<SkinnedMeshRenderer>();
            for (int i = 0; i < transAmOutput; i++)
            {
                var transAmObj = Instantiate(transAmGururin);
                gururinClones.Add(transAmObj.GetComponent<SkinnedMeshRenderer>());
            }
            transAmCount = transAmDensity;
            prePosition = transform.position;
            var gururinMesh = new Mesh();
            gururin.gear.GetComponent<SkinnedMeshRenderer>().BakeMesh(gururinMesh);
            /*
            var transAmShader = gururinClones[0].materials[0].shader;
            var defultColor = gururinClones[0].material.color;
            var alphaDelta = 0.2f / transAmOutput;
            */
            for (int i = 0; i < gururinClones.Count; i++)
            {
                gururinClones[i].sharedMesh = gururinMesh;
                gururinClones[i].gameObject.SetActive(false);
                /*
                var color = new Color(defultColor.r, defultColor.g, defultColor.b, 1 - i * alphaDelta);
                var mat = new Material(transAmShader);
                var renderType = false ? "Transparent" : "Fade";
                mat.SetOverrideTag("RenderType", renderType);
                mat.color = color;
                gururinClones[i].material = mat;
                */
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Debug.Log(Mathf.Abs(gururin.GetMoveVelocity()));
            DeltaUpdate();
            FootSmokeAction();
            if (Mathf.Abs(gururin.GetMoveVelocity()) > transAmLimitSpeed) TransAm();
            else
            {
                transAmCount = transAmDensity;
                for (int i = 0; i < gururinClones.Count; i++)
                {
                    if(gururinClones[i].gameObject.activeSelf) gururinClones[i].gameObject.SetActive(false);
                }
            }
        }

        private void DeltaUpdate()
        {
            if (prePosition != transform.position)
            {
                positionDelta = transform.position - prePosition;
                prePosition = transform.position;
            }
            if (preAngle != transform.eulerAngles)
            {
                angleDelta = transform.eulerAngles - preAngle;
                preAngle = transform.eulerAngles;
            }
        }

        private void FootSmokeAction()
        {
            if (gururin.CanJump && Mathf.Abs(gururin.GetMoveVelocity()) > footSmokeLimitSpeed)
            {
                if(footSmoke.isPlaying == false) footSmoke.Play();
            }
            else
            {
                if (footSmoke.isPlaying) footSmoke.Stop();
            }
        }

        private void TransAm()
        {
            if(transAmCount < transAmDensity)
            {
                transAmCount++;
            }
            else
            {
                if (gururinClones[0].gameObject.activeSelf == false) gururinClones[0].gameObject.SetActive(true);
                gururinClones[0].transform.position = transform.position - positionDelta;
                gururinClones[0].transform.rotation = transform.rotation;
                for (int i = 1;i<gururinClones.Count; i++)
                {
                    if (gururinClones[i].gameObject.activeSelf == false) gururinClones[i].gameObject.SetActive(true);
                    gururinClones[i].transform.position = gururinClones[i - 1].transform.position - positionDelta * (i+1);
                    gururinClones[i].transform.rotation = gururinClones[i - 1].transform.rotation;
                }
                transAmCount = 0;
            }
        }
    }
}


