using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public int damageNumber;
    [SerializeField] private Text displayNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayNumber.text = "" + damageNumber;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (moveSpeed * Time.deltaTime));
    }
}
