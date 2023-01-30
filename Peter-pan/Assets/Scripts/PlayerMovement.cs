using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 1000)] public float movementSpeed;
    [Range(0, 50)] public float jumpforce;

    public Rigidbody rb;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movements
        Movement();
    }

    private void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            //Debug.Log("bipbip");
        }
    }

    private void Movement()
    {
        float hMovement = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        float vMovement = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;
        velocity = new Vector3(hMovement, rb.velocity.y, vMovement);

        rb.velocity = velocity;
    }

    private void Jump()
    {
        velocity = new Vector3(velocity.x, jumpforce, velocity.z);
        rb.velocity = velocity;
    }
}
