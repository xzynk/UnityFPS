using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed;
    public float fireRate;
    
    private void Update()
    {
        if (speed == 0) return;
        
        var componentTransform = transform;
        componentTransform.position += componentTransform.forward * (speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
