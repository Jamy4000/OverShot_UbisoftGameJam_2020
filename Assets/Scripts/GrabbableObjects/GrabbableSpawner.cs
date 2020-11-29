using System.Collections.Generic;
using UbiJam.GrabbableObjects;
using UbiJam.Utils;
using UnityEngine;

namespace UbiJam.Player
{
    public class GrabbableSpawner : MonoSingleton<GrabbableSpawner>
    {
        [SerializeField]
        private GrabbablePool _flatPool;
        public GrabbablePool Pool { get { return _flatPool; } }

        protected override void Awake()
        {
            base.Awake();
            GameObject[] potentialSpawnPoints = GameObject.FindGameObjectsWithTag(TagsHolder.SpawnPointTag);

            List<int> randomIndexes = new List<int>(_flatPool.GrabbableObjectReferences.Count);
            int newIndex;

            for (int i = 0; i < randomIndexes.Capacity; i++)
            {
                while (true)
                {
                    newIndex = Random.Range(0, potentialSpawnPoints.Length - 1);
                    if (!randomIndexes.Contains(newIndex))
                    {
                        randomIndexes.Add(newIndex);
                        break;
                    }
                }
            }

            for (int i = 0; i < randomIndexes.Count; i++)
            {
                _flatPool.GrabbableObjectReferences[i].SceneGO.transform.position = potentialSpawnPoints[randomIndexes[i]].transform.position;
            }
        }
    }
}
