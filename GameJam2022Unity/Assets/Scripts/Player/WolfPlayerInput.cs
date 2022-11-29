using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfPlayerInput : MonoBehaviour, IPauseable
{
    [SerializeField]
    float mRunSpeed;
    [SerializeField]
    Scratch mEquippedWeapon;

    protected CharacterController2D mControllerComponent;
    protected float mHorizontalMovement = 0f;
    protected bool mJump;

    protected InteractorComponent mInteractorComponent;

    float EPSILON = .1f;

    public Action<bool, float> isMovingHorizontally;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void FixedUpdate()
    {
        isMovingHorizontally.Invoke(Math.Abs(mHorizontalMovement) > EPSILON, mHorizontalMovement);
        mControllerComponent.Move(mHorizontalMovement * Time.fixedDeltaTime, false, mJump);
        mJump = false;
    }
}
