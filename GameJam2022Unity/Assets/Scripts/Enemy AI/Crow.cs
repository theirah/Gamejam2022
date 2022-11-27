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
    [SerializeField] public float featherVelocity = 5.0f;
    [SerializeField] public GameObject chargeAttack;
    [SerializeField] private GameObject feather;
    [SerializeField] public float noticePlayerJumpForce = 100.0f;
    [SerializeField] public float m_MovementSmoothing = 0.05f;
    [SerializeField] private float m_MovementSpeed = 5f;

    private bool m_FacingRight = false;
    private Rigidbody2D rbody;
    private bool mCanSeePlayer = false;
    private Vector3 m_Velocity = Vector3.zero;
    private bool mIsCharging = false;
    private GameObject mCurrentFeather = null;
    

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
        else
        {
            mCanSeePlayer = false;
            Flap();
        }
    }

    private void Flap()
    {
        //TODO
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
            if (health.MaxHealth != health.CurrHealth)
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
}
