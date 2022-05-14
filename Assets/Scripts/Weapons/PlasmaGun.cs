using Core.Shooting;
using UnityEngine;

namespace Weapons
{
    public class PlasmaGun : WeaponBehaviour
    {
        [SerializeField]
        private GameObject bullet;

        protected override void OnGunShot()
        {
            var objTransform = transform;
            var instBullet = Instantiate(bullet, objTransform.position, objTransform.rotation);
            var rigidBullet = instBullet.GetComponent<Rigidbody>();

            rigidBullet.velocity = transform.forward * bulletSpeed;
        }
    }
}