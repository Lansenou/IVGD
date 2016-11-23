using UnityEngine;

namespace Assets.Scripts.Util
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;

        public static T Instance()
        {
            return instance;
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = GetClassType();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(transform.root.gameObject);
        }

        protected abstract T GetClassType();
    }
}