using Interfaces;
using Patterns.EventBus;
using Patterns.EventBus.Events;
using System;
using UnityEngine;

namespace Systems
{
    public class Health : MonoBehaviour, IDamagable
    {
        public event Action DeadEvent;
        [SerializeField] private int maxHealth;
        [SerializeField] private bool isEnemy;
        private int _currentHealth;
        private bool _isAlive;
        public bool IsEnemy => isEnemy;

        private void Start()
        {
            EventBus.Instance.Subscribe<Restart>((_) =>
            {
                _currentHealth = maxHealth;
                _isAlive = true;
            });
            _currentHealth = maxHealth;
            _isAlive = true;
        }


        public void TakeDamage(float damage)
        {
            if (_isAlive)
            {
                _currentHealth -= (int)damage;

                _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
                EventBus.Instance?.Invoke
                    (new HealthChangeEvent(_currentHealth));

                if (_currentHealth <= 0) Die();
            }
        }

        private void Die()
        {
            if (_isAlive)
            {
                _isAlive = false;
                DeadEvent?.Invoke();
            }
        }
    }
}
