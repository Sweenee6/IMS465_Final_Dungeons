using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private string goldTag;
    private objectPooler objPooler;

    private void Start()
    {
        objPooler = objectPooler.Instance;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            // Spawn Enemies at Spawn points
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                objPooler.SpawnFromPool(goldTag, spawnPoints[i].position, Quaternion.identity);
            }
            // disable once used
            this.gameObject.SetActive(false);
        }
    }
}
