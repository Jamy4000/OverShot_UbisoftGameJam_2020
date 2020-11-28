using UnityEngine;

namespace UbiJam.Utils
{
    public abstract class MonoSingleton : MonoBehaviour
    {
        public static MonoSingleton Instance
        {
            get; private set;
        }

        public MonoSingleton()
        {
            if (Instance != null)
            {
                Debug.LogError("Another Instance for the " + this.GetType().Name + " alrady exist on go " + gameObject.name + ".");
                Destroy(this);
            }

            Instance = this;
        }
    }
}
