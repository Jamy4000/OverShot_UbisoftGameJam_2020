using UbiJam.Utils;

namespace UbiJam
{
    public class GameSettings : MonoSingleton<GameSettings>
    {
        public CharacterSettings CharacterSettings;
        public SlingshotSettings SlingshotSettings;
        public float GameTime = 60.0f;
        public bool UseCountdown = true;
        public int MaxNeighborAmount = 5;
        public float MinTimeBetweenNeighborSpawn = 6.0f;
    }
}
