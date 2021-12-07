using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyNav : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private Transform player;
    private Rigidbody rb;

    [SerializeField] private GameObject damageNumber;
    [SerializeField] private int enemyHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
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
                Destroy(this.gameObject);
            }

        }
        else if (collision.tag == "Player")
        {
            PlayerController P = player.GetComponent<PlayerController>();
            if (P != null)
            {
                int playerDamage = Random.Range(0, 5);
                P.Damage(playerDamage); // player takes damage 
                
                //Create Player Damage Number
                var PlayerDamNum = (GameObject)Instantiate(damageNumber, new Vector3(player.position.x, damageNumber.transform.position.y, player.position.z), damageNumber.transform.rotation);
                PlayerDamNum.GetComponent<FloatingNumbers>().damageNumber = playerDamage;

                //Destroy(this.gameObject); // enemy
            }

        }
    }
}
