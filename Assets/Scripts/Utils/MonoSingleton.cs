using UnityEngine;

namespace UbiJam.Utils
{
    /// <summary>
    /// Note: This class is NOT thread-safe by default. If we need a thread-safe singleton,
    /// we should make it as a separate class (to avoid paying lock overhead when not needed).
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class MonoSingleton<ScriptType> : MonoBehaviour where ScriptType : MonoBehaviour
    {
        private static ScriptType _instance = null;
        private static bool _missingErrorShown = false;

        private static void CheckInstance()
        {
            if (!_instance)
            {
                if (!_missingErrorShown)
                {
                    Debug.LogError("Trying to access Singleton " + typeof(ScriptType).Name + " which isn't in the scene!");
                    _missingErrorShown = true;
                }
            }
        }

        public static ScriptType Instance
        {
            get
            {
                CheckInstance();
                return _instance;
            }
        }

        public static bool Exists
        {
            get
            {
                return !ReferenceEquals(_instance, null);
            }
        }

        protected virtual void Awake()
        {
            if (!ReferenceEquals(_instance, null))
            {
                Destroy(this);
                return;
            }

            _instance = this as ScriptType;
        }

        protected virtual void OnEnable()
        {
            if (!ReferenceEquals(_instance, this as ScriptType))
            {
                Debug.LogError("Singleton " + typeof(ScriptType).Name + "  instantiated twice!");
            }
            if (!_instance)
            {
                _instance = this as ScriptType;
            }
        }

        protected virtual void OnDestroy()
        {
            if (ReferenceEquals(this, _instance))
            {
                _instance = null;
            }
        }
    }
}