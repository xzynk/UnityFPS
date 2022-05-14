using System.Collections;
using Core.Character;
using Scriptable.Weapons;
using UnityEngine;

namespace Core.Shooting
{
    public class WeaponBehaviour : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private protected WeaponScriptable gunData;

        [SerializeField]
        private Transform firePoint;

        private float _timeSinceLastShot;
        private protected float bulletSpeed;

        private void Awake()
        {
            PlayerShoot.shootInput += Shooting;
            PlayerShoot.reloadInput += StartReload;

            bulletSpeed = gunData.speed;
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
            gunData.reloading = true;

            yield return new WaitForSeconds(gunData.reloadTime);

            gunData.currentAmmo = gunData.magSize;
            gunData.reloading = false;
        }

        private bool CanShoot()
        {
            return !gunData.reloading && _timeSinceLastShot > 1f / (gunData.fireRate / 60f);
        }

        private void Shooting()
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

        protected virtual void OnGunShot()
        {
        }

        private void OnDisable()
        {
            PlayerShoot.shootInput -= Shooting;
            PlayerShoot.reloadInput -= StartReload;
        }
    }
}