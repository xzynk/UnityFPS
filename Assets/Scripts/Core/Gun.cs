using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace CatCode.Core
{
    public class Gun : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GunData gunData;
        [SerializeField] private Transform firePoint;

        private float _timeSinceLastShot;

        private void Awake()
        {
            PlayerShoot.ShootInput += Shoot;
            PlayerShoot.ReloadInput += StartReload;
        }

        private void Update()
        {
            _timeSinceLastShot += Time.deltaTime;
            
        }

        private void StartReload()
        {
            if (!gunData.reloading)
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(gunData.reloadTime);
            
            gunData.currentAmmo = gunData.magSize;
            gunData.reloading = false;
        }

        private bool CanShoot()
        {
            return !gunData.reloading && _timeSinceLastShot > 1f / (gunData.fireRate/ 60f);
        }

        private void Shoot()
        {
            if (gunData.currentAmmo <= 0) return;
            if (!CanShoot()) return;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out var hitInfo, gunData.maxDistance))
            {
                Debug.Log((hitInfo.transform.name));
            }

            gunData.currentAmmo--;
            _timeSinceLastShot = 0;
            OnGunShot();
        }

        private void OnGunShot()
        {
            
        }

        private void OnDisable()
        {
            PlayerShoot.ShootInput -= Shoot;
            PlayerShoot.ReloadInput -= StartReload;
        }
    }
}
