using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeTrapped : MonoBehaviour
{
    private bool mIsTrapped = false;
    private int trappedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrapEntity trap = collision.gameObject.GetComponent<TrapEntity>();
        if (trap == null)
        {
            return;
        }
        else
        {
            Trap(trap);
        }

    }

    void Trap(TrapEntity trap)
    {
        trap.OnTrap();
        mIsTrapped = true;
        trappedTime = trap.trapTime;
        //Disable movement
        CharacterController2D control = GetComponent<CharacterController2D>();
        if (control != null)
        {
            control.enabled = false;
        }
        PlayerInputComponent playerinput = GetComponent<PlayerInputComponent>();
        if (playerinput != null)
        {
            playerinput.enabled = false;
        }
        SwapCharacterComponent swap = transform.parent.GetComponent<SwapCharacterComponent>();
        if (swap != null)
        {
            swap.enabled = false;
        }

        //Stop the player's momentum
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

    }

    private void FixedUpdate()
    {
        if (mIsTrapped)
        {
            if (trappedTime > 0)
            {
                trappedTime--;
                if (trappedTime <= 0)
                {
                    //Re-enable movement
                    CharacterController2D control = GetComponent<CharacterController2D>();
                    if (control != null)
                    {
                        control.enabled = true;
                    }
                    PlayerInputComponent playerinput = GetComponent<PlayerInputComponent>();
                    if (playerinput != null)
                    {
                        playerinput.enabled = true;
                    }
                    SwapCharacterComponent swap = transform.parent.GetComponent<SwapCharacterComponent>();
                    if (swap != null)
                    {
                        swap.enabled = true;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
