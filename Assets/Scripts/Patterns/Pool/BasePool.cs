using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.Pool
{
    public abstract class BasePool<T> : MonoBehaviour where T : IPoolable<T>
    {
        [SerializeField] protected float countInstanceWithStart;
        protected Queue<T> _instances = new Queue<T>();
        public virtual void Init()
        {
            for (int i = 0; i < countInstanceWithStart; i++)
            {
                var instance = CreateInstance();
                _instances.Enqueue(instance);
            }
        }
        public abstract T CreateInstance();
        public virtual T Get()
        {
            if (_instances.Count > 0)
            {
                T instance = _instances.Dequeue();
                instance.ReturnInPoolEvent += ReturnInPool;
                return instance;
            }
            else
            {
                T instance = CreateInstance();
                instance.ReturnInPoolEvent += ReturnInPool;
                return instance;
            }
        }
        public void ReturnInPool(T instance)
        {
            instance.Stop();
            instance.ReturnInPoolEvent -= ReturnInPool;
            _instances.Enqueue(instance);
        }
    }
}

