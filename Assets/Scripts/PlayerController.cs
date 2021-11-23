using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /* if (rb.GetComponent<Rigidbody>().gravityScale == 0f)
         {
             rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
         }
         else
         {
             rb.AddForce(direction.normalized * 15f);
         }*/
        rb.AddForce(direction.normalized * 15f);

    }

    public void OnMove(InputValue val)
    {
        var dir = val.Get<Vector2>();
        direction = new Vector3(dir.x, 0.0f ,dir.y);
    }

    public void OnFire()
    {
      /* var rayColor = Color.red;
        var rayDist = 10.0f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, rayDist);

        if (hit.collider != null)
        {
            Debug.Log("Hit " + hit.collider);
            rayColor = Color.green;
            
            //change color of hit
            hit.collider.gameObject.GetComponent<SpriteRenderer>().color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        }

        Debug.DrawRay(transform.position, Vector2.up * rayDist, rayColor, 1.0f);

        /*ray = new Ray(transform.position, Vector2.up);
        var rayColor = Color.red;
        var rayDist = 10.0f;
        if (Physics2D.Raycast(ray., out var hit, rayDist))
        {
            rayColor = Color.green;
            Destroy(hit.collider.GetComponent<MeshRenderer>());
        }
        Debug.DrawRay(ray.origin, ray.direction * rayDist, rayColor, 1.0f);
    */
    }

}
