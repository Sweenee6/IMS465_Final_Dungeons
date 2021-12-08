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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            //flip direction when hitting a wall
            direction = -direction;
        }
    }

}
