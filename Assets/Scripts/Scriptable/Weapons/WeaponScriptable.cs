using UnityEngine;

namespace Scriptable.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable/Weapon")]
    public class WeaponScriptable : ScriptableObject
    {
        [Header("Info")]
        public new string name;

        [Header("Shooting")]
        public float damage;
        public float maxDistance;

        [Header("Reloading")]
        public int currentAmmo;
        public int magSize;
        public float fireRate;
        public float reloadTime;

        [Header("Ammo")]
        public float speed;

        [HideInInspector]
        public bool reloading;

    }
}
