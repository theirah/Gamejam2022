using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField] public int attackDistance = 3;
    [SerializeField] public GameObject chargeAttack;
    [SerializeField] public float noticePlayerJumpForce = 100.0f;
    [SerializeField] public float m_MovementSmoothing = 0.05f;
    [SerializeField] private float m_MovementSpeed = 5f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_GroundCheck2;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character

    private bool m_FacingRight = false;
    private Rigidbody2D rbody;
    private bool mCanSeePlayer = false;
    private Vector3 m_Velocity = Vector3.zero;
    

    // Start is called before the first frame update
    void Start()
    {
        rbody= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleAI();
        KeepUpright();
    }

    private void HandleAI()
    {
        //Check if the enemy can see the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (hit.collider.gameObject.tag == "Player")
        {
            //We've seen the player, make sure we're facing in the right direction
            if ((hit.transform.position.x < transform.position.x) && m_FacingRight)
            {
                Flip();
            }
            else if ((hit.transform.position.x > transform.position.x) && !m_FacingRight)
            {
                Flip();
            }

            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = (hit.transform.position - transform.position).normalized * m_MovementSpeed;
                // And then smoothing it out and applying it to the character
                rbody.velocity = Vector3.SmoothDamp(rbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                
            }
        }
        else
        {
            mCanSeePlayer = false;
            Flap();
        }
    }

    private void Flap()
    {
        //Move the crow up and down a bit as if it was flapping
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void KeepUpright()
    {
        transform.rotation = Quaternion.identity;
    }
}
