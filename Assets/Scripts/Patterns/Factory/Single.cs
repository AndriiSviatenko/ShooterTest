using UnityEngine;

namespace Patterns.Factory
{
    public class Single<T> : Factory<T> where T : MonoBehaviour
    {
        [SerializeField] protected T prefab;
        public override T Spawn()
        {
            var instance = Instantiate(prefab);
            return instance;
        }
    }
}
