using Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        public event Action<Bullet> ReturnInPoolEvent;

        [SerializeField] private float damage;
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody rb;
        private Vector3 _direction;


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamagable damagable))
            {
                rb.velocity = Vector3.zero;
                damagable.TakeDamage(damage);
                ReturnInPool();
            }
            ReturnInPool();
        }

        public void Setup(Vector3 direction)
        {
            _direction = direction;
        }
        public void Play()
        {
            gameObject.SetActive(true);
            Move();
            StartCoroutine(Dead());
        }

        private IEnumerator Dead()
        {
            yield return new WaitForSeconds(5f);
            ReturnInPool();
        }
        public void ReturnInPool()
        {
            ReturnInPoolEvent?.Invoke(this);
        }
        public void Move()
        {
            rb.AddForce(_direction * speed);
        }

        public void Stop()
        {
            gameObject.SetActive(false);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
