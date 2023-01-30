using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Space(20f)]
    [Header("References")]

    //Refs
    [SerializeField] private GameObject attackArea = default;
    [SerializeField] private Animator animator;

    public Rigidbody rb;

    [Space(25f)]
    [Header("Animations")]

    //Animation States
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_STAB = "Stab";
    const string PLAYER_SLASH = "Inward Slash";
    const string PLAYER_HEAVYATTACK = "Heavy Attack";
    const string PLAYER_BLOCK = "Block";
    const string PLAYER_RUN = "Slow Run";
    const string PLAYER_DEATH = "Death";
    const string PLAYER_TAUNT = "Taunt Military";

    [Space(20f)]
    [Header("Stats")]

    //Stats
    [SerializeField] [Range(0, 10)] private float timeToAttack = 0.25f;
    [Range(0, 1000)] public float movementSpeed;
    [Range(0, 50)] public float jumpforce;

    [Space(20f)]
    [Header("Others to verify")]

    //Others
    [SerializeField] private bool attacking = false;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private int groundMask;
    [SerializeField] private string currentState;
    [SerializeField] [Range(0, 10)] private float timer = 0f;
    [SerializeField] private Vector3 velocity = Vector3.zero;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        attackArea = transform.GetChild(0).gameObject;

        //Be sure to be idle from the start
        ChangeAnimationState(PLAYER_IDLE);
    }

    private void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            //Debug.Log("bipbip");
        }

        //Input actions
        if (Input.GetKeyDown(KeyCode.A))
        {
            Block();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Stab();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Slash();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            HeavyAttack();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Taunt();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Death();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }

    void FixedUpdate()
    {
        //Get Horizontal & Vertical Axis
        float hMovement = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        float vMovement = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

        Debug.Log(hMovement);
        Debug.Log(vMovement);

        //Change the animation state from Run to Idle & Idle to Run
        if (hMovement >= 0.25f || vMovement >= 0.25f)
        {
            ChangeAnimationState(PLAYER_RUN);
        }
        else if (hMovement <= -0.25f || vMovement <= -0.25f)
        {
            ChangeAnimationState(PLAYER_RUN);
        }
        else if(hMovement >= -0.25f && vMovement >= -0.25f && hMovement <= 0.25f && vMovement <= 0.25f)
        {
            ChangeAnimationState(PLAYER_IDLE);
        }

        //Movements
        Movement(hMovement, vMovement);

        //Ground check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, groundMask);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Movement(float _hMovement, float _vMovement)
    {
        velocity = new Vector3(_hMovement, rb.velocity.y, _vMovement);

        rb.velocity = velocity;
        
    }

    private void Jump()
    {
        velocity = new Vector3(velocity.x, jumpforce, velocity.z);
        rb.velocity = velocity;
    }

    private void Stab()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        ChangeAnimationState(PLAYER_STAB);
    }
    
    private void Taunt()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        ChangeAnimationState(PLAYER_TAUNT);
    }

    private void Slash()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        ChangeAnimationState(PLAYER_SLASH);
    }

    private void HeavyAttack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        ChangeAnimationState(PLAYER_HEAVYATTACK);
    }

    private void Block()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        ChangeAnimationState(PLAYER_BLOCK);
    }

    private void Death()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        ChangeAnimationState(PLAYER_DEATH);
    }

    private void ChangeAnimationState(string newState)
    {
        //Prevent an animation to override itself
        if (currentState == newState) return;

        //Play the new animation
        animator.Play(newState);

        //Change the current state animation to the new animation
        currentState = newState;
    }
}
