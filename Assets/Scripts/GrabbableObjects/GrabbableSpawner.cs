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
    }
}
