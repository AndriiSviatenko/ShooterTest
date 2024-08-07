using UnityEngine;

namespace Patterns.EventBus.Events
{
    public class DeadEvent<T> where T : MonoBehaviour
    {
        [field:SerializeField] public T Object { get; private set; }
        public DeadEvent(T obj) 
        {
            Object = obj;
        }
    }
}
