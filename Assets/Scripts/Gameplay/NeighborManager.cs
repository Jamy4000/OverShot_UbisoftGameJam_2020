using System.Collections.Generic;
using UbiJam.Events;
using UbiJam.GrabbableObjects;
using UnityEngine;

namespace UbiJam.Gameplay
{
    public class NeighborManager : MonoBehaviour
    {
        private GameObject[] _windows;
        private List<GrabbableReceiver> _currentReceivers = new List<GrabbableReceiver>();
        private GameSettings _settings;
        private float _timeSinceLastSpawn = 0.0f;

        private void Awake()
        {
            OnGameStarted.Listeners += StartSystem;
            OnGameEnded.Listeners += EndSystem;
        }

        private void Start()
        {
            _settings = GameSettings.Instance;
            _windows = GameObject.FindGameObjectsWithTag(Utils.TagsHolder.WindowTag);
            GenerateNeighbors(_settings.NeighborsOnStart);
            this.enabled = false;
        }

        private void Update()
        {
            if (_currentReceivers.Count == _settings.MaxNeighborAmount)
                return;

            _timeSinceLastSpawn += Time.deltaTime;
            if (_timeSinceLastSpawn > _settings.MinTimeBetweenNeighborSpawn)
            {
                int random = Random.Range(0, 100);
                if (random >= 90)
                    GenerateNeighbors(1);
            }
        }

        private void OnDestroy()
        {
            OnGameStarted.Listeners -= StartSystem;
            OnGameEnded.Listeners -= EndSystem;
        }

        private void GenerateNeighbors(int amount)
        {
            _timeSinceLastSpawn = 0.0f;

            for (int i = 0; i < amount; i++)
            {
                int randomIndex;
                GrabbableReceiver receiver;

                while (true)
                {
                    randomIndex = Random.Range(0, _windows.Length - 1);
                    receiver = _windows[randomIndex].GetComponent<GrabbableReceiver>();

                    if (!receiver.IsActive)
                    {
                        _currentReceivers.Add(receiver);
                        receiver.ActivateReceiver((EGrabbableObjects)Random.Range(1, (int)(EGrabbableObjects.COUNT - 1)));
                        break;
                    }
                }

                if (_currentReceivers.Count == _settings.MaxNeighborAmount)
                    return;
            }
        }

        private void StartSystem(OnGameStarted info)
        {
            this.enabled = true;
        }

        private void EndSystem(OnGameEnded info)
        {
            this.enabled = false;
        }
    }
}
