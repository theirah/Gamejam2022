using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Crow : MonoBehaviour
{
    [SerializeField] public float attackDistance = 3.0f;
    [SerializeField] public float featherAttackDistance = 20.0f;
    [SerializeField] public float featherVelocity = 15.0f;
    [SerializeField] public int flapTime = 30;
    [SerializeField] public float flapStrength = 1.0f;
    [SerializeField] public GameObject peckAttack;
    [SerializeField] private GameObject feather;
    [SerializeField] public float noticePlayerJumpForce = 100.0f;
    [SerializeField] public float m_MovementSmoothing = 0.05f;
    [SerializeField] private float m_MovementSpeed = 5f;

    private bool m_FacingRight = false;
    private Rigidbody2D rbody;
    private Vector3 m_Velocity = Vector3.zero;
    private bool mIsCharging = false;
    private GameObject mCurrentFeather = null;
    private GameObject mCurrentPeck = null;
    private float? mLastHealth = null;

    private bool mFlapUp = false;
    private int mFlapCounter = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
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

    //The idea for this AI is that the crow will shoot feathers at the player until it takes damage, at which point it will charge them.
    private void HandleAI()
    {
        //Check if the enemy can see the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (hit.collider.gameObject.tag == "Player")
        {
            if (mIsCharging == false)
                CheckIfDamaged();
            //We've seen the player, make sure we're facing in the right direction
            if ((hit.transform.position.x < transform.position.x) && m_FacingRight)
            {
                Flip();
            }
            else if ((hit.transform.position.x > transform.position.x) && !m_FacingRight)
            {
                Flip();
            }

            if (mIsCharging)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = (hit.transform.position - transform.position).normalized * m_MovementSpeed;
                // And then smoothing it out and applying it to the character
                rbody.velocity = Vector3.SmoothDamp(rbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                //If the player is close enough to attack
                if (Vector3.Magnitude(hit.transform.position - transform.position) < attackDistance)
                {
                    //Debug.Log("Crow attacking!");
                    if (mCurrentPeck == null)
                    {
                         PeckAttack(hit);
                    }
                }
                else if (Vector3.Magnitude(hit.transform.position - transform.position) > featherAttackDistance)
                {
                    //Crow is out of range, go back to shooting mode
                    mLastHealth = GetComponent<HealthComponent>().CurrHealth;
                    mIsCharging = false;
                    rbody.velocity = Vector3.zero;
                }
            }
            else
            {
                //Fire a feather at the player
                if (Vector3.Magnitude(hit.transform.position - transform.position) < featherAttackDistance)
                {
                    FireFeather(hit);
                }
            }
        }

        if (!mIsCharging)
        {
            Flap();
        }
    }

    private void Flap()
    {
        if (mFlapUp)
        {
            rbody.velocity = Vector3.up * flapStrength;
            mFlapCounter++;
            if (mFlapCounter > flapTime)
                mFlapUp = false;
        }
        else
        {
            rbody.velocity = Vector3.down * flapStrength;
            mFlapCounter--;
            if (mFlapCounter < 0)
                mFlapUp = true;
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

    private void KeepUpright()
    {
        transform.rotation = Quaternion.identity;
    }

    private void CheckIfDamaged()
    {
        HealthComponent health = GetComponent<HealthComponent>();
        if (health == null) 
        {
            Debug.LogError("No health component attached to crow");
            return;
        }
        else
        {
            if (mLastHealth == null)
            {
                mLastHealth = health.MaxHealth;
            }
            if (mLastHealth != health.CurrHealth)
            {
                mIsCharging = true;
            }
        }
    }

    private void FireFeather(RaycastHit2D hit)
    {
        //Make sure there's not already a fired feather
        if (mCurrentFeather != null)
        {
            return;
        }
        Vector3 direction = (hit.transform.position - transform.position).normalized;
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);
        float angle = Mathf.Rad2Deg * angleInRadians;
        Quaternion rot = Quaternion.Euler(0, 0, angle);
        mCurrentFeather = Instantiate(feather, transform.position, rot);
        mCurrentFeather.GetComponent<Rigidbody2D>().velocity = mCurrentFeather.transform.rotation * (featherVelocity * new Vector3(1, 0, 0));
    }

    void PeckAttack(RaycastHit2D hit)
    {
        if (peckAttack == null)
        {
            Debug.LogError("No attack found");
            return;
        }
        Vector3 direction = (hit.transform.position - transform.position).normalized;
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);
        float angle = Mathf.Rad2Deg * angleInRadians;
        Quaternion rot = Quaternion.Euler(0, 0, angle);
        mCurrentPeck = Instantiate(peckAttack, this.transform.position, rot);
        
        Vector3 theScale = mCurrentPeck.transform.localScale;
        theScale.x *= -1;
        mCurrentPeck.transform.localScale = theScale;
    }
}
