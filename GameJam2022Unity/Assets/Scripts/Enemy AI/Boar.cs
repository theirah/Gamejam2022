using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : MonoBehaviour
{
    [SerializeField] public int attackDistance = 3;
    [SerializeField] public GameObject chargeAttack;
    [SerializeField] public float noticePlayerJumpForce = 100.0f;
    [SerializeField] public float m_MovementSmoothing = 0.05f;
    [SerializeField] private float m_MovementSpeed = 5f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private int maxCoyoteTime = 2;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private GameObject chargeAttackInstance = null;
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
        CheckGrounded();
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
                if (chargeAttackInstance == null)
                {
                    ChargeAttack();
                }
            }
            else
            {
                if (m_Grounded)
                {
                    // Move the character by finding the target velocity
                    Vector3 targetVelocity = (hit.transform.position - transform.position).normalized * m_MovementSpeed;
                    // And then smoothing it out and applying it to the character
                    rbody.velocity = Vector3.SmoothDamp(rbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                }
                else if (coyoteTime > 0)
                {
                    //Allow the boar some horizontal movement just after it's left the ground
                    coyoteTime--;
                    Vector3 targetVelocity = new Vector3((hit.transform.position - transform.position).normalized.x * m_MovementSpeed, 0);
                    rbody.velocity = Vector3.SmoothDamp(rbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                }
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

    void ChargeAttack()
    {
        if (chargeAttack == null)
        {
            Debug.LogError("No attack found");
            return;
        }

        chargeAttackInstance = Instantiate(chargeAttack, this.transform.position, this.transform.rotation);
        if (m_FacingRight)
        {
            Vector3 theScale = chargeAttackInstance.transform.localScale;
            theScale.x *= -1;
            chargeAttackInstance.transform.localScale = theScale;
        }
    }

    private void CheckGrounded()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger)
            {
                m_Grounded = true;
                coyoteTime = maxCoyoteTime;
            }
        }
    }

    private void KeepUpright()
    {
        transform.rotation = Quaternion.identity;
    }
}
