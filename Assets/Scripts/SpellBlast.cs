using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBlast : MonoBehaviour, IPooledObject
{

    private Rigidbody rb;
    [SerializeField] private float force = 10.0f;
  
        private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnObjectSpawn()
    {
        rb.velocity = Vector3.zero;
        //apply force

        /*Vector3 v = (launcherPos.position - transform.position).normalized * force;
        rb.AddForce(v, ForceMode.Impulse);*/

        rb.rotation = transform.rotation;
        rb.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);

    }

  /*  private void OnDisable()
    {
        rb.transform.rotation = Quaternion.Euler(Vector3.zero);
        objPooler.poolD.Requeue(gameObject);
    }*/
}