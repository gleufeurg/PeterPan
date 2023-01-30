using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private string currentState;

    [SerializeField] private const string PIRATE_Death = "Death";

    private void Start()
    {
        animator.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Dead");
        ChangeAnimationState(PIRATE_Death);
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

}
