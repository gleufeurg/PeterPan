using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 10f;
    public float jumpforce = 100f;

    public Rigidbody rb;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Movement();
    }

    private void Movement()
    {
        float hMovement = Input.GetAxisRaw("Horizontal") * MovementSpeed * Time.deltaTime;
        float vMovement = Input.GetAxisRaw("Vertical") * MovementSpeed * Time.deltaTime;
        velocity = new Vector3(hMovement,rb.velocity.y, vMovement);

        rb.velocity = velocity;
    }

    private void Jump()
    {
        
    }
}
