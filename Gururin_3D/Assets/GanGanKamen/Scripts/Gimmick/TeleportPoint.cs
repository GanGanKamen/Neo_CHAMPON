using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class TeleportPoint : MonoBehaviour
    {
        [Header("現在パート")] [SerializeField] GameObject nowPart;
        [Header("移動先")] [SerializeField] Transform destination;
        [Header("移動先パート")] [SerializeField] GameObject nextPart;
        [Header("フェードイン時間")] [SerializeField] float fadeinTime;
        [Header("待ち時間")] [SerializeField] float waitTime;
        [Header("フェードアウト時間")] [SerializeField] float fadeoutTime;
        [Header("隠扉")] [SerializeField] bool extra;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<PlayerCtrl>();
                StartCoroutine(TransportProcess(player));
            }
        }

        private IEnumerator TransportProcess(PlayerCtrl player)
        {
            player.MoveStop();
            player.ProhibitControll();
            Fader.FadeInAndOutBlack(fadeinTime, waitTime, fadeoutTime);
            nextPart.SetActive(true);
            yield return new WaitForSeconds(fadeinTime + waitTime / 2);
            var pos = new Vector3(destination.position.x, destination.position.y, 0);
            player.transform.position = pos;
            yield return new WaitForSeconds(waitTime / 2 + fadeoutTime);
            if (extra) nowPart.SetActive(false);
            else Destroy(nowPart);
            player.PermitControll();
            yield break;
        }
    }
}


