using Interfaces;
using System;
using UnityEngine;

namespace Weapon
{
    public abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        public event Action<int, int> AmmoChangeEvent;
        public event Action ShootEvent;

        [SerializeField] protected float maxRange = 2f;
        [SerializeField] protected float damage = 1f;
        [SerializeField] protected int _currentAmmo;
        protected bool _reloading;

        protected float _timeSinceLastShot;

        public virtual void Shoot()
        {
            if (CanShoot())
            {
                ShootEvent?.Invoke();
                if (Physics.Raycast(Camera.main.transform.position,
                        Camera.main.transform.forward, out var hitInfo, maxRange))
                {
                    Debug.Log(hitInfo.transform.gameObject.name);

                    if (hitInfo.transform.TryGetComponent(out IDamagable damagable))
                    {
                        if (damagable != null)
                        {
                            if (damagable.IsEnemy)
                            {
                                damagable.TakeDamage(damage);
                            }
                        }

                    }

                }
            }
        }
        public void OnAmmoChange(int currentAmmo, int maxAmmo)
        {
            AmmoChangeEvent?.Invoke(currentAmmo, maxAmmo);
        }

        public abstract bool CanShoot();

        public abstract void Reload();
    }
}
