using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]

    //Refs
    [SerializeField] private GameObject attackArea = default;
    [SerializeField] private Animator animator;

    public Rigidbody rb;

    [Space(20f)]
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
    [Range(0, 1000)] public float movementSpeed;
    [Range(0, 50)]   public float jumpforce;

    [Space(20f)]
    [Header("Others to verify")]

    //Others
    [SerializeField] private bool attacking = false;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isMoving;
    [SerializeField] private int groundMask = 3;
    [SerializeField] private string currentState;
    [SerializeField] [Range(0, 10)] private float timer = 0f;
    [SerializeField] [Range(0, 10)] private float stabDelay = 0.75f;
    [SerializeField] [Range(0, 10)] private float slashDelay = 1f;
    [SerializeField] [Range(0, 10)] private float timeToAttack = 0.25f;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private LayerMask whatIsGround;


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

        #region Inputs Actions

        if (Input.GetKeyDown(KeyCode.A))
        {
            Block();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(AttackDelay(stabDelay));
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

        #endregion

        //Hitboxe attacking timer
        //Depend of the attack
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
        //Movements
        Movement();

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

    private void Movement()
    {
        //Get Horizontal & Vertical Axis
        float hMovement = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        float vMovement = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

        velocity = new Vector3(hMovement, rb.velocity.y, vMovement);
        rb.velocity = velocity;

        //Change the animation state from Run to Idle & Idle to Run
        if (hMovement >= 0.25f || vMovement >= 0.25f || hMovement <= -0.25f || vMovement <= -0.25f)
        {
            ChangeAnimationState(PLAYER_RUN);
        }
    }

    #region Actions

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
        attackArea.SetActive(attacking);
        ChangeAnimationState(PLAYER_BLOCK);
    }

    private void Death()
    {
        attackArea.SetActive(attacking);
        ChangeAnimationState(PLAYER_DEATH);
    }

    #endregion

    private IEnumerator AttackDelay(float _delay)
    {
        print("YAAAAAAH !!!");
        yield return new WaitForSeconds(_delay);
        Stab();
        print("stabbed");
        //For the example I call stab here but it has to be optimised later
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
