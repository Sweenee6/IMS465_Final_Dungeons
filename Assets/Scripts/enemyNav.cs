using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyNav : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private Transform player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);

       /* if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion angle = Quaternion.LookRotation(direction); // find angle of enemy to player
            rb.rotation = angle;
        }*/
    }
}
