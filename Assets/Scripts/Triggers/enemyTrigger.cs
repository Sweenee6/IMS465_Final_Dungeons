using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private string enemyTag;

    private objectPooler objPooler;

    private void Start()
    {
        objPooler = objectPooler.Instance;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            // Spawn Enemies at Spawn points
            for (int i = 0; i < enemySpawnPoints.Length; i++)
            {
                GameObject enemyClone = objPooler.SpawnFromPool(enemyTag, enemySpawnPoints[i].position, Quaternion.identity);

                enemyClone.transform.position = enemySpawnPoints[i].position;
                //var enemyClone = (GameObject)Instantiate(enemyPrefab, enemySpawnPoints[i].position, Quaternion.identity);

                // Enemies target player that spawned them
                //enemyClone.GetComponent<enemyNav>().player = collision.transform;

            }
            // disable once used
            this.gameObject.SetActive(false);
        }
    }
}
