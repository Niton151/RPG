using System;
using UnityEngine;

namespace Game.Scripts
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    Type t = typeof(T);
                    _instance = (T) FindObjectOfType(t);
                    if (!_instance)
                    {
                        Debug.LogError(t + " is nothing.");
                    }
                }

                return _instance;
            }
        }
    }
}
