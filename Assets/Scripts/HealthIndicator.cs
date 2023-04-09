using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HealthIndicator : MonoBehaviour
{
    public Health health;
    
    public int damage;
    private Material mat;
    
    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        FindObjectOfType<Health>().OnHealthChanged += HealthIndicator_OnHealthChanged;
    }
    
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
    
    private void HealthIndicator_OnHealthChanged(float healthPercentage)
    {
        mat.SetFloat("_Cutoff", 1f - healthPercentage);
    }   
}
