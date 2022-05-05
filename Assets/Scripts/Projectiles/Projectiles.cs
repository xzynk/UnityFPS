using Core;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace CatCode.Projectiles
{
    public class Projectiles : MonoBehaviour
    {
        private void Update()
        {
            var componentTransform = transform;
            componentTransform.position += componentTransform.forward * (50 * Time.deltaTime);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}
