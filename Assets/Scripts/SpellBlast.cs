using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBlast : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float force = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody and apply force
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
        
    }


}