using UnityEngine;

namespace Patterns.Factory
{
    public abstract class Factory<T> : MonoBehaviour where T : MonoBehaviour
    {
        public abstract T Spawn();
    }
}
