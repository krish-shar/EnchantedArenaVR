using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text healthScore;
    public PhotonView PV;

    public void Win(float currentHealth) {
        gameObject.SetActive(true);
        if (currentHealth <= 0) {
            healthScore.text = "You lost. Try again?";
        }
        else {
            healthScore.text = "You won! Good job.";
        }
    }
    
    [PunRPC]
    public void RPC_checkWin(float currentHealth)
    {
        if (PV.IsMine)
            return;
        gameObject.SetActive(true);
        healthScore.text = "You won! Good job.";
    }

}