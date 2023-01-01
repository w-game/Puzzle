using UnityEngine;

namespace Common
{
    public class SMonoSingleton<T> : MonoBehaviour where T : Object
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Object.FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        _instance = new GameObject(nameof(T), typeof(T)).GetComponent<T>();
                    }
                }

                return _instance;
            }
        }
    }
}