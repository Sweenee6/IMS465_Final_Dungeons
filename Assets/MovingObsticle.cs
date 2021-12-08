using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObsticle : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        //if a wall is hit flip direction
        if(other.tag == "Wall")
        {
            Debug.Log("Wall");
            direction = -direction;
        }
    }
}
