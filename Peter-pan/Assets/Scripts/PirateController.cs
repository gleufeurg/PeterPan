using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{
    [Header("References")]

    //Refs
    [SerializeField] private GameObject attackArea = default;
    [SerializeField] private Collider hurtbox;
    [SerializeField] Animator animator;

    [Space(20f)]
    [Header("Animations")]

    //Animation States
    //const string PIRATE_IDLE = "Idle";
    //const string PIRATE_RUN = "Slow Run";
    const string PIRATE_STAB = "Stab";
    const string PIRATE_SLASH = "Inward Slash";
    const string PIRATE_HEAVYATTACK = "Heavy Attack";
    const string PIRATE_BLOCK = "Block";
    const string PIRATE_DEATH = "Death";

    [Space(20f)]
    [Header("Others to verify")]

    //Others
    [SerializeField] private bool attacking = false;
    [SerializeField] private string currentState;
    [SerializeField] private const string PIRATE_Death = "Death";
    [SerializeField] [Range(0, 10)] private float timer = 0f;
    [SerializeField] [Range(0, 10)] private float timeToAttack = 0.25f;


    private void Start()
    {
        animator.GetComponent<Animator>();
        hurtbox.GetComponent<Collider>();
        attackArea = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        //Hitboxe timer when attacking
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Dead");
        Death();
    }

    #region Actions

    private void Stab()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        ChangeAnimationState(PIRATE_STAB);
    }

    private void Slash()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        ChangeAnimationState(PIRATE_SLASH);
    }

    private void HeavyAttack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        ChangeAnimationState(PIRATE_HEAVYATTACK);
    }

    private void Block()
    {
        ChangeAnimationState(PIRATE_BLOCK);
    }

    private void Death()
    {
        ChangeAnimationState(PIRATE_DEATH);
    }

    #endregion

    //Call this Method to change/play animation
    //Name of the new animation in parameter
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

}
