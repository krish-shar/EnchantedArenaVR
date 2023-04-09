using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MagicManager : MonoBehaviour
{
    
    [SerializeField]
    MovementRecognizer movementRecognizer;
    
    public Transform spawnPoint;

    public GameObject fireball;
    public GameObject lightning;
    public GameObject freeze;
    public GameObject arcane;
    
    public float spawnDistance = 1f;
    
    
    public void spawnFireball()
    {
        Vector3 playerPosition = Camera.main.transform.position;
        Quaternion playerRotation = Camera.main.transform.rotation;

        // Calculate spawn position in front of player
        Vector3 spawnPosition = playerPosition + playerRotation * Vector3.forward * spawnDistance;

        // Spawn item at spawn position
       GameObject fb = PhotonNetwork.Instantiate("Fireball", spawnPosition, Quaternion.identity);
       StartCoroutine(removeProjectile(fb, 5));
    }
    public void spawnLightning()
    {
        Vector3 playerPosition = Camera.main.transform.position;
        Quaternion playerRotation = Camera.main.transform.rotation;

        // Calculate spawn position in front of player
        Vector3 spawnPosition = playerPosition + playerRotation * Vector3.forward * spawnDistance;

        // Spawn item at spawn position
        //Instantiate(lightning, spawnPosition, Quaternion.identity);
        GameObject ln = PhotonNetwork.Instantiate( "Lightning", spawnPosition, Quaternion.identity);
        StartCoroutine(removeProjectile(ln, 5));
    }
    
    public void spawnFreeze()
    {
        Vector3 playerPosition = Camera.main.transform.position;
        Quaternion playerRotation = Camera.main.transform.rotation;

        // Calculate spawn position in front of player
        Vector3 spawnPosition = playerPosition + playerRotation * Vector3.forward * spawnDistance;

        // Spawn item at spawn position
        //Instantiate(freeze, spawnPosition, Quaternion.identity);
        GameObject fe = PhotonNetwork.Instantiate("Freeze", spawnPosition, Quaternion.identity);
        StartCoroutine(removeProjectile(fe, 5));
    }
    
    public void spawnArcane()
    {
        Vector3 playerPosition = Camera.main.transform.position;
        Quaternion playerRotation = Camera.main.transform.rotation;

        // Calculate spawn position in front of player
        Vector3 spawnPosition = playerPosition + playerRotation * Vector3.forward * spawnDistance;

        // Spawn item at spawn position
        GameObject ac = PhotonNetwork.Instantiate("Arcane", spawnPosition, Quaternion.identity);
        StartCoroutine(removeProjectile(ac, 5));
    }

    IEnumerator removeProjectile(GameObject projectile, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        PhotonNetwork.Destroy(projectile);
    }


}
