using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject goldPrefab;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            // Spawn Enemies at Spawn points
            for (int i = 0; i < spawnPoints.Length; i++)
            {
               Instantiate(goldPrefab, spawnPoints[i].position, Quaternion.identity);
            }
            // disable once used
            this.gameObject.SetActive(false);
        }
    }
}
