using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyNav : MonoBehaviour, IPooledObject
{
    [SerializeField] private NavMeshAgent enemy;
    //[SerializeField] private Transform[] player;
    private Rigidbody rb;

    [SerializeField] private GameObject damageNumber;
    [SerializeField] private int enemyHealth = 5;
    [SerializeField] private int phase2Health = 0; // set to zero if no phase 2
    [SerializeField] private GameObject shootpoint = null;
    [SerializeField] private string phase2projectile = null;
    [SerializeField] private float throwForce = 0f;
    [SerializeField] private bool Phase2 = false;
    private float timebetweenShots;
    [SerializeField] private float startTimeBtwShots = 20f;

    private objectPooler objPooler;
    private gameManager GM;

    [SerializeField] private string goldDrop = null;

    [SerializeField]private bool isBoss = false;
    private UIManager UI = null;

    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        GM = gameManager.Instance;
        /*player[0] = GameObject.Find("Player").transform;
        player[1] = GameObject.Find("Player2").transform;*/
        if (isBoss){ UI = GameObject.Find("Canvas").GetComponent<UIManager>(); }
        objPooler = objectPooler.Instance;
        

        //timebetweenShots = startTimeBtwShots;
        timebetweenShots = 0f;
    }

    public void OnObjectSpawn()
    {
        enemy.Warp(transform.position);

        rb.rotation = transform.rotation;
        //rb.position = transform.position;
        //transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        var pTransforms = GM.playerTransfroms;
        if (pTransforms.Length > 0)
        {
            Transform closestPlayer = pTransforms[0];
            float dist = Vector3.Distance(transform.position, pTransforms[0].position);
            
            for(int i=0; i < pTransforms.Length; i++)
            {
                if (pTransforms[i] != null)
                {
                    var tempDist = Vector3.Distance(transform.position, pTransforms[i].position);

                    if (tempDist < dist)
                    {
                        closestPlayer = pTransforms[i];
                        dist = tempDist;
                    }
                }
            }
            if (closestPlayer != null)
            {
                enemy.SetDestination(closestPlayer.position);
            }

            /*int target = 0;

            if (Vector3.Distance(player[1].position, transform.position) < Vector3.Distance(player[0].position, transform.position))
            {
                target = 1;
            }

            enemy.SetDestination(player[target].position);*/

        }

        if (Phase2)
        {
            if (timebetweenShots <= 0 && shootpoint!=null)
            {
                //launch enemies at players
                //var launchClone = (GameObject)Instantiate(phase2projectile, shootpoint.transform.position, shootpoint.transform.rotation);
                var launchClone = objPooler.SpawnFromPool(phase2projectile, shootpoint.transform.position, shootpoint.transform.rotation);
                launchClone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * throwForce, ForceMode.Impulse);

                timebetweenShots = startTimeBtwShots;//reset shot timer

            } else
            {
                timebetweenShots -= Time.deltaTime;
            }
            
        }

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
            //Destroy(collision.gameObject); //Fireball and acidblast
            collision.gameObject.SetActive(false);
            //Destroy(this.gameObject); // enemy
            
            // Random Damage to Enemy
            int damageAmount = Random.Range(1, 5);
            enemyHealth = enemyHealth - damageAmount;

            // Create damage number
            //var EnemyDamNum = (GameObject) Instantiate(damageNumber, new Vector3(transform.position.x, damageNumber.transform.position.y, transform.position.z), damageNumber.transform.rotation);
            var EnemyDamNum = objPooler.SpawnFromPool("damNum", new Vector3(transform.position.x, damageNumber.transform.position.y, transform.position.z), damageNumber.transform.rotation);
            EnemyDamNum.GetComponent<FloatingNumbers>().damageNumber = damageAmount;

            if (enemyHealth <= phase2Health)
            {
                Phase2 = true;
            }
            //If no health destroy enemy
            if (enemyHealth <= 0)
            {
                objPooler.SpawnFromPool(goldDrop, transform.position, transform.rotation);

                if (isBoss && UI != null)
                {
                    UI.showWinText();
                }

                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);
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
                //var PlayerDamNum = (GameObject)Instantiate(damageNumber, new Vector3(collision.transform.position.x, damageNumber.transform.position.y, collision.transform.position.z), damageNumber.transform.rotation);

                var PlayerDamNum = objPooler.SpawnFromPool("damNum", new Vector3(collision.transform.position.x, damageNumber.transform.position.y, collision.transform.position.z), damageNumber.transform.rotation);
                PlayerDamNum.GetComponent<FloatingNumbers>().damageNumber = playerDamage;

                //Destroy(this.gameObject); // enemy
            }

        }
    }
}
