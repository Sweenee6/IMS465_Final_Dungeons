using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int playerNum;
    private Rigidbody rb;
    [SerializeField] private Camera cam;
    [SerializeField]
    private float speed;
    private Vector3 direction;
    [SerializeField] private int health = 10;
    public Transform startPosition;

    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    [SerializeField] private bool isGamepad;
    private Vector2 aim;

    [SerializeField] private Transform FirePoint;
    //[SerializeField] private GameObject SpellPrefab = null;
    private float fireRate = 0.25f; // slowdown
    private float canFire = 0.05f; // elapsed time

    private UIManager UI = null;
    private objectPooler objPooler;
    private PlayerInput pi;
    private gameManager GM = null;

    void Awake()
    {
        GM = gameManager.Instance;
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        rb = GetComponent<Rigidbody>();
        UI.UpdateHealth(health, playerNum);
        objPooler = objectPooler.Instance;
        pi = GetComponent<PlayerInput>();
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    private void Start()
    {
        GM.OnPlayerJoin(gameObject);//Set player values
        transform.position = startPosition.position;//Start at spawn
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
    }

    private void FixedUpdate()
    {
        //move Player
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        
    }

    public void OnMove(InputValue val)
    {
        var dir = val.Get<Vector2>();
        direction = new Vector3(dir.x, 0.0f ,dir.y);
    }

    public void OnFire()
    {
        if (Time.time > canFire)
        {
            //Instantiate(SpellPrefab, FirePoint.position, FirePoint.rotation);
            if (FirePoint != null) { objPooler.SpawnFromPool("spell", FirePoint.position, FirePoint.rotation); }

            canFire = Time.time + fireRate; //shot delay
        }

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

    */
    }

    void handleRotation()
    {
        if (isGamepad)
        {
            //Rotate player with sticks
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
        else // if not gamepad
        {
            // rotate with mouse position
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                point.y = transform.position.y;
                rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(point - transform.position), gamepadRotateSmoothing * 10 * Time.deltaTime);
            }
        }
    }

    public void OnAim(InputValue val)
    {
        aim = val.Get<Vector2>();
    }

    private void lookAt(Vector3 lookpoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookpoint.x, rb.position.y, lookpoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void Damage(int damageAmount)
    {
        health = health - damageAmount;

        if (health <= 0)
        {
            transform.position = startPosition.position;
            health = 10;
        }

        UI.UpdateHealth(health, playerNum);
    }

    public void OnControlsChanged(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}
