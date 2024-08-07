using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.EventBus
{
    public class EventBus : MonoBehaviour
    {
        public static EventBus Instance { get; private set; }

        private Dictionary<string, List<object>> _signalCallbacks = new Dictionary<string, List<object>>();
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }
        }

        public void Subscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks[key].Add(callback);
            }
            else
            {
                _signalCallbacks.Add(key, new List<object>() { callback });
            }
        }
        public void Unsubscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks[key].Remove(callback);
            }
            else
            {
                Debug.LogErrorFormat("Unsubscribe failed");
            }
        }
        public void Invoke<T>(T signal)
        {
            string key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                foreach (var obj in _signalCallbacks[key])
                {
                    var callback = obj as Action<T>;
                    callback?.Invoke(signal);
                }
            }
        }
    }
}

