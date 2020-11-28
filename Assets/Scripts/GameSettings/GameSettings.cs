using UbiJam.Utils;

namespace UbiJam
{
    public class GameSettings : MonoSingleton<GameSettings>
    {
        public CharacterSettings CharacterSettings;
        public SlingshotSettings SlingshotSettings;
    }
}
