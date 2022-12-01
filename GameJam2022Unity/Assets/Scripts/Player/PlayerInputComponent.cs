using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputComponent : MonoBehaviour
{
    [SerializeField]
    float mRunSpeed;

    protected CharacterController2D mControllerComponent;
    protected float mHorizontalMovement = 0f;
    protected bool mJump;

    protected InteractorComponent mInteractorComponent;

    float EPSILON = .1f;
    public Action<bool, float> isMovingHorizontally;
    public Action hasJumped;

    void Awake()
    {
        mControllerComponent = GetComponent<CharacterController2D>();
        mInteractorComponent = GetComponent<InteractorComponent>();
    }
    void Update()
    {
        mHorizontalMovement = Input.GetAxisRaw("Horizontal") * mRunSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            mJump = true;
        }
        if (Input.GetButtonDown("Interact"))
        {
            mInteractorComponent.TryInteract();
        }

    }
    void FixedUpdate()
    {
        isMovingHorizontally.Invoke(Math.Abs(mHorizontalMovement) > EPSILON, mHorizontalMovement);
        if (mJump) hasJumped.Invoke();
        mControllerComponent.Move(mHorizontalMovement * Time.fixedDeltaTime, false, mJump);
        mJump = false;
    }
}
