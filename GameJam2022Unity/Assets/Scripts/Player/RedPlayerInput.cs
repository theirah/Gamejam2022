using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayerInput : MonoBehaviour
{
    [SerializeField]
    float mRunSpeed;

    [SerializeField]
    Bow mEquippedWeapon;

    protected CharacterController2D mControllerComponent;
    protected float mHorizontalMovement = 0f;
    protected bool mJump;

    protected InteractorComponent mInteractorComponent;
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
        if (Input.GetButton("Fire"))
        {
            mEquippedWeapon.AddCharge(Time.deltaTime);
        }
        if (Input.GetButtonUp("Fire"))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z; // distance between camera and grid, whose position is at 0
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mEquippedWeapon.Fire(gameObject.transform.position, targetPosition);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mControllerComponent.Move(mHorizontalMovement * Time.fixedDeltaTime, false, mJump);
        mJump = false;
    }


}
