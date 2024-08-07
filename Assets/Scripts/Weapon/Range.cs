using Inputs;
using Patterns.EventBus;
using Patterns.EventBus.Events;
using System;
using System.Collections;
using Systems;
using UnityEngine;

namespace Weapon
{
    public class Range : BaseWeapon
    {
        public event Action StartReloadEvent;
        public event Action EndReloadEvent;

        [SerializeField] private InputProvider inputProvider;
        [SerializeField] private EffectController effectController;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int maxAmmo;
        [SerializeField] private float fireRate;
        [SerializeField] private float reloadTime;

        private void Start()
        {
            _currentAmmo = maxAmmo;
            OnAmmoChange(_currentAmmo, maxAmmo);
            inputProvider.ShootClickEvent += Shoot;
            inputProvider.ReloadClickEvent += StartReload;
            ShootEvent += () => effectController.EnableEffect(spawnPoint.position);
        }

        private void Update()
        {
            _timeSinceLastShot += Time.deltaTime;
            if (_currentAmmo <= 0)
            {
                EventBus.Instance.Invoke<AmmoStatus>(new AmmoStatus(false));
            }
            else
            {
                EventBus.Instance.Invoke<AmmoStatus>(new AmmoStatus(true));
            }
        }

        public bool IsAmmoEquals => _currentAmmo == maxAmmo;

        public void StartReload()
        {
            Debug.Log("Reloading!");
            if (!_reloading)
            {
                StartReloadEvent?.Invoke();
                Reload();
            }
        }

        public override bool CanShoot()
        {
            return !_reloading
                && _timeSinceLastShot > 1 / (fireRate / 60f)
                && _currentAmmo > 0;
        }

        public override void Shoot()
        {
            base.Shoot();
            _currentAmmo--;
            _timeSinceLastShot = 0;
            OnAmmoChange(_currentAmmo, maxAmmo);
        }

        public override void Reload()
        {
            StartCoroutine(ReloadCoroutine());
        }
        private IEnumerator ReloadCoroutine()
        {
            _reloading = true;
            yield return new WaitForSeconds(reloadTime);
            _currentAmmo = maxAmmo;
            OnAmmoChange(_currentAmmo, maxAmmo);
            _reloading = false;
            EndReloadEvent?.Invoke();
        }
    }
}
