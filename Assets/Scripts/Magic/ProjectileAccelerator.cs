using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAccelerator : MonoBehaviour
{
    
public float acceleration = 1.5f;
public Rigidbody rb;

// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rb.velocity.magnitude < 15 && rb.velocity.magnitude > 0.1)
            transform.position += transform.forward * acceleration * Time.deltaTime;
        
    }
}
