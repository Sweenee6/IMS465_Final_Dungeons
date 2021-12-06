using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Camera cam;
    [SerializeField]
    private float speed;
    private Vector3 direction;

    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    [SerializeField] private bool isGamepad;
    private Vector3 AimMousePos;
    private Vector2 aim;
    private Vector2 movement;
    private PlayerControls playerControls;
    private PlayerInput playerInput;

    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject SpellPrefab = null;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //playerControls = new PlayerControls();
        //playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        //handleInput();
    }

    void handleInput()
    {
        //aim = playerControls.Player.Aim.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //move Player
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        //rb.AddForce(direction.normalized * 15f);
        
    }

    public void OnMove(InputValue val)
    {
        var dir = val.Get<Vector2>();
        direction = new Vector3(dir.x, 0.0f ,dir.y);
    }

    public void OnFire()
    {
        Instantiate(SpellPrefab, FirePoint.position, FirePoint.rotation);
        

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

    void handleRotation()
    {
        if (isGamepad)
        {
            //Rotate player
            if (Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            /*Vector3 lookDir = AimMousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg - 90f;*/


            //Ray ray = Camera.main.ScreenPointToRay(aim);
            //Ray ray = cam.ScreenPointToRay(aim);
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                point.y = transform.position.y;
                rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(point - transform.position), gamepadRotateSmoothing * 10 * Time.deltaTime);
                //rb.rotation = Quaternion.LookRotation(point - transform.position);

                //lookAt(point);
            }
            
            
            
                
                /*float rayDistance;
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
           
                //lookAt(point);
            }*/
        }
    }

    public void OnAim(InputValue val)
    {

        aim = val.Get<Vector2>();

        if (isGamepad)
        {
            AimMousePos = cam.ScreenToWorldPoint(new Vector3(aim.x, 0f, aim.y));
        }
        
       /* if (isGamepad)
        {
            //Rotate player
            if (Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    
                    rb. = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            *//*Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                lookAt(point);
            }*//*

            

            AimMousePos = Camera.main.ScreenToWorldPoint(new Vector3(aim.x, 0.0f, aim.y));

            Vector3 direction = AimMousePos - transform.position;
            Quaternion angle = Quaternion.LookRotation(direction);
            rb.rotation = angle;
        }*/
    }

    private void lookAt(Vector3 lookpoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookpoint.x, rb.position.y, lookpoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }

}
