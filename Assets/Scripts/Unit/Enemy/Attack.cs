using Patterns.Pool.Bullet;
using System;
using System.Collections;
using UnityEngine;

namespace Unit.Enemy
{
    public class Attack : MonoBehaviour
    {
        public event Action AttackEvent;

        [SerializeField] private Transform spawnPos;
        [SerializeField] private float timeBetweenAttacks;

        private BulletPool _bulletPool;
        private bool _alreadyAttacked;
        public void SetPool(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }
        public void AttackPlayer()
        {
            if (!_alreadyAttacked)
            {
                _alreadyAttacked = true;
                var bullet = _bulletPool.Get();
                bullet.SetPosition(spawnPos.position);
                bullet.Setup(transform.forward);
                bullet.Play();
                AttackEvent?.Invoke();
                StartCoroutine(ResetAttack());
            }
        }

        private IEnumerator ResetAttack()
        {
            yield return new WaitForSeconds(timeBetweenAttacks);
            _alreadyAttacked = false;
        }
    }
}