using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    float mRunSpeed;

    protected CharacterController2D mControllerComponent;
    protected float mHorizontalMovement = 0f;
    protected bool mJump;

    // Start is called before the first frame update
    void Awake()
    {
        mControllerComponent = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        mHorizontalMovement = Input.GetAxisRaw("Horizontal") * mRunSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            mJump = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mControllerComponent.Move(mHorizontalMovement * Time.fixedDeltaTime, false, mJump);
        mJump = false;
    }
}
