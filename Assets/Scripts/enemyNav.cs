using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyNav : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private Transform[] player;
    private Rigidbody rb;

    [SerializeField] private GameObject damageNumber;
    [SerializeField] private int enemyHealth = 5;

    [SerializeField] private GameObject goldDrop = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player[0] = GameObject.Find("Player1").transform;
        player[1] = GameObject.Find("Player2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        int target = 0;

        if (Vector3.Distance(player[1].position, transform.position) < Vector3.Distance(player[0].position, transform.position))
        {
            target = 1;
        }

        enemy.SetDestination(player[target].position);

       /* if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion angle = Quaternion.LookRotation(direction); // find angle of enemy to player
            rb.rotation = angle;
        }*/
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Projectile")
        {
            Destroy(collision.gameObject); //Fireball and acidblast
                                           //Destroy(this.gameObject); // enemy
            // Random Damage to Enemy
            int damageAmount = Random.Range(1, 5);
            enemyHealth = enemyHealth - damageAmount;

            // Create damage number
            var EnemyDamNum = (GameObject) Instantiate(damageNumber, new Vector3(transform.position.x, damageNumber.transform.position.y, transform.position.z), damageNumber.transform.rotation);
            EnemyDamNum.GetComponent<FloatingNumbers>().damageNumber = damageAmount;

            //If no health destroy enemy
            if(enemyHealth <= 0)
            {
                Instantiate(goldDrop, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

        }
        else if (collision.tag == "Player")
        {
            PlayerController P = collision.GetComponent<PlayerController>();
            if (P != null)
            {
                int playerDamage = Random.Range(0, 5);
                P.Damage(playerDamage); // player takes damage 
                
                //Create Player Damage Number
                var PlayerDamNum = (GameObject)Instantiate(damageNumber, new Vector3(collision.transform.position.x, damageNumber.transform.position.y, collision.transform.position.z), damageNumber.transform.rotation);
                PlayerDamNum.GetComponent<FloatingNumbers>().damageNumber = playerDamage;

                //Destroy(this.gameObject); // enemy
            }

        }
    }
}
