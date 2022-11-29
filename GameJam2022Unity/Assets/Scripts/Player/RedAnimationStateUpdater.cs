using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAnimationStateUpdater : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    RedPlayerInput playerInput;

    bool mIsMovingHorizontally;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput.isMovingHorizontally += HandleIsMovingHorizontally;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsWalking", mIsMovingHorizontally);
    }

    public void HandleIsMovingHorizontally(bool isMoving, float axisInput)
    {
        mIsMovingHorizontally = isMoving;
    }
}
