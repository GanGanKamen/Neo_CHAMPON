using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Igarashi
{
    public class HoistSEPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip hoistSE;
        [SerializeField] private HoistCrane hoistCrane;

        private AudioSource _audioSource;
        private GanGanKamen.GameController _gameController;
        private bool _playsSE;

        // Start is called before the first frame update
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = hoistSE;
            _audioSource.loop = true;
            _audioSource.spatialBlend = 1.0f;
            _gameController = GameObject.Find("GameController").GetComponent<GanGanKamen.GameController>();
        }

        // Update is called once per frame
        void Update()
        {
            SEState();
        }

        private void SEState()
        {
            if (hoistCrane.Limit != 0 || (_gameController.InputLongPress && hoistCrane.EngagesWithGear))
            {
                _audioSource.Stop();
                _playsSE = false;
            }
            else if (hoistCrane.Limit == 0 && _playsSE == false)
            {
                _audioSource.Play();
                _playsSE = true;
            }
        }
    }
}
