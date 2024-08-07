using System;
using UnityEngine;

namespace Interfaces
{
    public interface IPoolable<T>
    {
        event Action<T> ReturnInPoolEvent;
        void Play();
        void SetPosition(Vector3 position);
        void ReturnInPool();
        void Stop();
    }
}


