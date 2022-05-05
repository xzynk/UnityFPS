using UnityEngine;
namespace Core
{
    public class Weapons : MonoBehaviour
    {
        //public float damage;
        
        public class Weapon
        {
            public float fireRate;
            public static float bulletSpeed;
            public Transform firePoint;
            public GameObject projectile;

            private Camera _mainCamera;
            private Transform _cameraPosition;

            public Weapon()
            {
                
            }
        }
        private void Start()
        {
            //if (Camera.main != null) _mainCamera = Camera.main;
        }
        /*
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            GetCameraPosition();
            Shoot();
        }
        
        private void GetCameraPosition()
        {
            _cameraPosition = _mainCamera.transform;
        }

        private void Shoot()
        {
            if (Physics.Raycast(_cameraPosition.position, _cameraPosition.forward, out var hit, 100f))
            {
                firePoint.LookAt(hit.point);
            }
            
            Instantiate(projectile, firePoint.position, firePoint.rotation);

        }
        */
    }
    
}
