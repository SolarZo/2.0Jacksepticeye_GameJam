using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.DebugUI.Table;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMechanics : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    private InputAction sprintAction;
    private PlayerInput pi;


    public Vector3 move;
    //Vector3 PlayerMove;
    public GameObject RespawnAnchor;
    public Transform spawnPoint;
    public float speedMultiplier = 5f;
    public float maxSpeed = 3f;
    public float throwSpeed = 3f;
    public float launchAngle = 45f;
    public float jumpForce = 500f;

    public bool onWall = false;
    public bool onSlope = false;
    public bool isSprinting = false;

    public Vector3 lastDirection = new Vector3(1f, 0, 0);
    bool onGround = true;

    private void Start()
    {
        sprintAction = playerControls.FindAction("Sprint");
        sprintAction.canceled += OnSprintCanceled;
        sprintAction.Enable();

    }

    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        //setup Spawner Spawn
        Vector3 origin = (spawnPoint ? spawnPoint.position : transform.position + Vector3.up);
        //Throw respawn
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Walker walkerscript = GetComponentInChildren<Walker>();
            if (!walkerscript.isThrowing)
            {
                ThrowLantern(origin, new Vector3(lastDirection.x, 0, 0));
                walkerscript.StartThrowAnimation();
            }
        }

        if (onSlope)
        {
            speedMultiplier = 11f;
            maxSpeed = 7.5f;
        }
        else
        {
            speedMultiplier = 5f;
            maxSpeed = 3f;
        }

        if (isSprinting)
        {
            speedMultiplier = 10f;
            maxSpeed = 6.3f;
        }
        else
        {
            speedMultiplier = 5f;
            maxSpeed = 3f;
        }


        //Player Movement
        if (rb.linearVelocity.x > maxSpeed && !onWall)
        {
            rb.linearVelocity = new Vector3(maxSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        else if (rb.linearVelocity.x < -maxSpeed && !onWall)
        {
            rb.linearVelocity = new Vector3(-maxSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        else if (rb.linearVelocity.z > maxSpeed && !onWall)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, maxSpeed);
        }
        else if (rb.linearVelocity.z < -maxSpeed && !onWall)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, -maxSpeed);
        }
        else if (!onWall || onGround)
        {
            if (!onSlope)
                rb.AddForce(move * speedMultiplier);
            else
                rb.AddForce((move + new Vector3(0, .25f, 0)) * speedMultiplier);
        }
        //Debug.Log("player on ground: " + onGround);
    }

    void OnMove(InputValue value)
    {
        Vector2 moveValue = value.Get<Vector2>();

        move = new Vector3(moveValue.x, 0, moveValue.y);
        if (move.x != 0)
            lastDirection = new Vector3(move.x, 0, 0);
        turnPlayerModel();
    }

    void turnPlayerModel()
    {
        Transform modelTransform = GetComponentInChildren<Transform>();
        if (move.x > 0)
            modelTransform.rotation = Quaternion.Euler(0, 0, 0);
        else if (move.x < 0)
            modelTransform.rotation = Quaternion.Euler(0, -180, 0);
    }

    void OnSprint(InputValue value)
    {
        isSprinting = true;
        Debug.Log("Sprint Started");
    }
    void OnSprintCanceled(InputAction.CallbackContext context)
    {
        isSprinting = false;
        Debug.Log("Sprint Canceled");
    }
    void OnJump(InputValue value)
    {
        if (onGround)
        {
            Debug.Log("jumping");
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("GodCube"))
        {
            Debug.Log("Collided with GodCube");
            GameManager.Instance.EndGame();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            onWall = true;
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            //Debug.Log("Collided with floor");
            onGround = true;
        }


    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Slope"))
        {
            onSlope = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Slope"))
        {
            onSlope = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            onWall = false;
        }
    }


    public GameObject ThrowLantern(Vector3 origin, Vector3 direction)
    {
        //remove old Respawner
        GameObject OldRespawnLantern = GameObject.FindGameObjectWithTag("Respawn");
        if (OldRespawnLantern != null)
            Destroy(OldRespawnLantern);



        if (!RespawnAnchor)
        {
            Debug.LogWarning("PlayerMechanics: No projectilePrefab assigned.");
            return null;
        }
        direction = direction.normalized;

        //Spawn Lantern
        GameObject projectile = Instantiate(RespawnAnchor, origin, Quaternion.LookRotation(Vector3.forward));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();


        Vector3 inherit = Vector3.zero;
        Rigidbody myRb = GetComponent<Rigidbody>();
        if (myRb) inherit = Vector3.zero;//myRb.linearVelocity;


        Vector3 axis = Vector3.Cross(direction, Vector3.up);
        Vector3 launchdirection = Quaternion.AngleAxis(launchAngle, axis) * direction;



        Debug.Log("launch direction: " + launchdirection + " * " + throwSpeed + " :Throw speed");
        rb.AddForce(launchdirection * throwSpeed, ForceMode.Impulse);
        //rb.linearVelocity = launchdirection * throwSpeed;

        Collider[] myCols = GetComponentsInChildren<Collider>();
        Collider[] projCols = projectile.GetComponentsInChildren<Collider>();
        foreach (var a in projCols)
            foreach (var b in myCols)
                if (a && b) Physics.IgnoreCollision(a, b, true);






        return projectile;
    }

}
