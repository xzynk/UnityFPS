using System.Collections;
using Core.Character;
using Scriptable.Weapons;
using UnityEngine;

namespace Core.Shooting
{
    public class Weapon : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private WeaponScriptable gunData;

        [SerializeField] private Transform firePoint;

        private float _timeSinceLastShot;

        private void Awake()
        {
            PlayerShoot.ShootInput += Shooting;
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

        private void OnGunShot()
        {
            print("shoot");
        }

        private void OnDisable()
        {
            PlayerShoot.ShootInput -= Shooting;
            PlayerShoot.ReloadInput -= StartReload;
        }
    }
}