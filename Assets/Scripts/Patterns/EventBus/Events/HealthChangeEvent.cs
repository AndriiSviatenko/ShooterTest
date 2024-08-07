using UnityEngine;

namespace Patterns.EventBus.Events
{
    public class HealthChangeEvent
    {
        [field: SerializeField] public int Health { get; private set; }
        public HealthChangeEvent(int health)
        {
            Health = health;
        }
    }
}
