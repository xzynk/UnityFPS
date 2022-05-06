using UnityEngine;

namespace CatCode
{
    public class BulletController : MonoBehaviour
    {

        public float speed, bulletLife;
        public Rigidbody myRigidBody;
    
        private void Update()
        {
            BulletMovement();
            bulletLife -= Time.deltaTime;

            if (bulletLife <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void BulletMovement()
        {
            myRigidBody.velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}
