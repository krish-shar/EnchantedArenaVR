using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class MagicProjectile : MonoBehaviour
{
    public float damage;

    public float criticalChance;
    
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    
    

    // check if the projectile hits a trigger collider with tag "Player"
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!photonView.IsMine)
            {
                if (UnityEngine.Random.Range(0f, 1f) < (criticalChance / 100))
                {
                    damage *= 1.5f;
                }

                // if it does, get the player's health component and call the TakeDamage function
                other.GetComponent<Health>().TakeDamage(damage);
                // destroy the projectile
                Destroy(gameObject);
            }
        }
    }
    
    
}
