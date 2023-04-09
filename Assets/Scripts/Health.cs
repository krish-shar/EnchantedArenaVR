using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{ 
    public float maxHealth = 100;
    [Range(0,100)]
    public float currentHealth;
    
    public event Action<float> OnHealthChanged;
    public TMP_Text winText;
    public Transform spawnPoint;
    public GameObject player;
  
    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        float healthPercentage = (float)currentHealth / maxHealth;
        OnHealthChanged?.Invoke(healthPercentage);
        
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            endGame();
        }
    }

    [PunRPC]
    public void endGame()
    {
        player.transform.position = spawnPoint.position;

        if (currentHealth <= 0)
        {
            winText.text = "You win!";
        }
        else
        {
            winText.text = "You lose!";
        }
        
    }
}
