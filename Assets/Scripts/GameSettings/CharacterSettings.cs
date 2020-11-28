using UnityEngine;

namespace UbiJam
{
    [CreateAssetMenu(menuName = "UbiJam/Create Character Settings")]
    public class CharacterSettings : ScriptableObject
    {
        public float PlayerWalkSpeed = 5.0f;
        public float PlayerRunSpeed = 7.0f;
    }
}
