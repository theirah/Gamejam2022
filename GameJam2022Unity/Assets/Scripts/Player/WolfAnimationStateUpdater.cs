using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnimationStateUpdater : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    PlayerInputComponent playerInput;

    [SerializeField]
    CharacterController2D characterController;

    bool mIsMovingHorizontally;
    bool mJumped;
    bool mWasGrounded;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput.isMovingHorizontally += HandleIsMovingHorizontally;
        playerInput.hasJumped += HandleHasJumped;
        mJumped = false;
        bool mWasGrounded = characterController.m_Grounded;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsWalking", mIsMovingHorizontally);
        animator.SetBool("IsGrounded", characterController.m_Grounded);
        bool hasLanded = !mWasGrounded && characterController.m_Grounded;
        mJumped = mJumped && !hasLanded;
        animator.SetBool("Jumped", mJumped);
        mWasGrounded = characterController.m_Grounded;
    }

    public void HandleIsMovingHorizontally(bool isMoving, float axisInput)
    {
        mIsMovingHorizontally = isMoving;
    }

    public void HandleHasJumped()
    {
        mJumped = true;
    }
}
