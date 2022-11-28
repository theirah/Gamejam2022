using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapper : MonoBehaviour
{
    [SerializeField] public float attackDistance = 10.0f;
    [SerializeField] public GameObject trap;
    [SerializeField] public float noticePlayerJumpForce = 100.0f;
    [SerializeField] public float m_MovementSmoothing = 0.05f;
    [SerializeField] private float m_MovementSpeed = 5f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_GroundCheck2;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private int maxCoyoteTime = 2;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private GameObject trapInstance = null;
    private bool m_FacingRight = false;
    private Rigidbody2D rbody;
    private bool mCanSeePlayer = false;
    private Vector3 m_Velocity = Vector3.zero;
    private bool m_Grounded;
    private int coyoteTime = 0;
    

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
    }

    private void HandleAI()
    {
        //Check if the enemy can see the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (hit.collider.gameObject.tag == "Player")
        {
            if (mCanSeePlayer == false)
            {
                // Add a jump to the boar as it has noticed the player
                rbody.AddForce(new Vector2(0f, noticePlayerJumpForce));
            }
            mCanSeePlayer = true;
            //We've seen the player, make sure we're facing in the right direction
            if ((hit.transform.position.x < transform.position.x) && m_FacingRight)
            {
                Flip();
            }
            else if ((hit.transform.position.x > transform.position.x) && !m_FacingRight)
            {
                Flip();
            }

            //If the player is close enough to attack
            if (Vector3.Magnitude(hit.transform.position - transform.position) < attackDistance)
            {
                //Debug.Log("Pig attacking!");
                if (trapInstance == null)
                {
                    LayTrap(hit);
                }
            }
            else
            {

            }
        }
        else
        {
            mCanSeePlayer = false;
            //Debug.Log("Pig can't see player");
        }
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

    void LayTrap(RaycastHit2D hit)
    {
        if (trap == null)
        {
            Debug.LogError("No attack found");
            return;
        }

        trapInstance = Instantiate(trap, hit.transform.position, this.transform.rotation);
        if (m_FacingRight)
        {
            Vector3 theScale = trapInstance.transform.localScale;
            theScale.x *= -1;
            trapInstance.transform.localScale = theScale;
        }
    }
}
