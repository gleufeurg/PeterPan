using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Privates")]
    [SerializeField] private bool endedJumpEarly = false;
    [SerializeField] [Range(0, 100)] private float addedFallSpeed;

    [Space(25)]

    [Header("Physics")]

    [Range(0,1000)] public float MovementSpeed;
    [Range(0, 50)]  public float jumpforce;
    [Range(0, 100)] public float fallSpeed;

    public Rigidbody rb;

    private Vector3 velocity = Vector3.zero;


    void Start()
    {
        //Get References
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Movements
        Movement();
    }

    private void Update()
    {
        //Jump
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
            //Debug.Log("Jumping");
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            endedJumpEarly = true;
        }
    }

    private void Movement()
    {
        float hMovement = Input.GetAxisRaw("Horizontal") * MovementSpeed * Time.deltaTime;
        float vMovement = Input.GetAxisRaw("Vertical") * MovementSpeed * Time.deltaTime;
        
        velocity = new Vector3(hMovement, velocity.y, vMovement);
        rb.velocity = velocity;

        Debug.Log("velocity.y = " + velocity.y);
    }

    private void Jump()
    {
        if (endedJumpEarly && rb.velocity.y > 0)
        {
            //velocity.y -= (addedFallSpeed + fallSpeed) * Time.deltaTime;
            velocity.y = 0;

            Debug.Log("Should have stopped");
        }
        else
        {
            velocity.y -= fallSpeed * Time.deltaTime;
            Debug.Log("velocity.y = " + velocity.y);
        }

        velocity = new Vector3(velocity.x, jumpforce, velocity.z);
        rb.velocity = velocity;
    }
}
