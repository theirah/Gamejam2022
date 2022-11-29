using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodcutter : MonoBehaviour
{
    //Locations
    [SerializeField] public Transform platform1;
    [SerializeField] public Transform platform2;
    [SerializeField] public Transform leftSide;
    [SerializeField] public Transform rightSide;
    [SerializeField] public Transform leftArena;
    [SerializeField] public Transform rightArena;

    //Attack objects
    [SerializeField] private GameObject phase1Trap;
    [SerializeField] private GameObject phase2Trap;
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject swing;
    [SerializeField] private GameObject grenade;

    private bool mFacingRight = true;
    private int mCurrentAttackTimer = 0;
    private bool mInAttack = false;
    private bool mInSecondPhase = false;

    //Attack/State parameters. I'm not making them public because they should never be used in another context
    //Movement
    private bool mIsMoving = false;
    const int mMovementTime = 40;
    private Vector3 mStartPosition;
    private Transform mTargetPosition = null;

    //Trapping variables
    private GameObject mFollowingTrap = null;
    const int trapAttackTime = 300;

    //Charge variables
    const int prepareTime = 60;
    const int chargeTime = 50;
    const int prepareJumpForce = 1000;

    //Axe throw variables
    const int throwTime = 100;
    private GameObject mAxe = null;
    private Vector3 mAxeStartPosition;
    private Transform mAxeTargetPosition = null;

    //Grenade variables
    const float throwStrength = 10.0f;
    const int bombTime = 300;

    //Fire Variables
    const int fireTime = 120;
    const int numberOfFlames = 10;
    private int mCurrentFlame = 0;
    private Vector3 mFireStartPosition;
    private Transform mFireTargetPosition = null;

    //Swinging variables
    const int swingTime = 60;
    private GameObject mAxeSwing = null;


    private enum states
    {
        trapping,
        swinging,
        charging,
        bombing,
        spreadFire,
        throwingAxe,
        moving,
        none
    }

    private states mCurrentState;
    private states mNextState;

    // Start is called before the first frame update
    void Start()
    {
        mCurrentState = states.none;
        mTargetPosition = rightSide;
    }

    private void FixedUpdate()
    {
        FacePlayer();
        HandleState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleState()
    {
        switch (mCurrentState)
        {
            case states.none:
                PickNextAttack();
                break;

            case states.trapping:
                HandleTrapping();
                break;

            case states.throwingAxe:
                HandleAxeThrow();
                break;

            case states.bombing:
                HandleBombing();
                break;

            case states.charging:
                HandleCharging();
                break;

            case states.spreadFire:
                HandleSpreadFire();
                break;

            case states.swinging:
                HandleSwinging();
                break;

            case states.moving:
                HandleMovement();
                break;

            default:
                mCurrentState = states.none;
                break;

        }
    }

    private void PickNextAttack()
    {
        bool valid = false;
        while (valid == false)
        {
            //Make sure not to do the same attack twice
            int nextAttack = Random.Range(0, 6);
            if (nextAttack != (int)mCurrentState)
            {
                valid = true;
                mNextState = (states)nextAttack;
                mCurrentState = states.moving;
            }
        }
    }

    private void HandleTrapping()
    {
        if (mInAttack == false)
        {
            mInAttack = true;
            mCurrentAttackTimer = trapAttackTime;
        }
        if (mFollowingTrap == null)
        {
            GameObject trap;
            if (mInSecondPhase)
                trap = phase2Trap;
            else
                trap = phase1Trap;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            mFollowingTrap = Instantiate(trap, player.transform.position, Quaternion.identity);
        }

        if (mCurrentAttackTimer % 100 == 0)
        {
            GameObject trap;
            if (mInSecondPhase)
                trap = phase2Trap;
            else
                trap = phase1Trap;
            int noOfTraps = Random.Range(2, 6);
            for (int i = 0; i < noOfTraps + 1; i++)
            {
                float xPos = leftArena.position.x + ((rightArena.position.x - leftArena.position.x) * ((float)i / (float)noOfTraps));
                Vector3 pos = new Vector3(xPos, leftArena.position.y);
                Instantiate(trap, pos, Quaternion.identity);
            }
        }

        mCurrentAttackTimer--;

        if (mCurrentAttackTimer < 0)
        {
            mInAttack = false;
            PickNextAttack();
        }   
    }

    void HandleCharging()
    {
        if (mInAttack == false)
        {
            mStartPosition = transform.position;
            mInAttack = true;
            mCurrentAttackTimer = chargeTime + prepareTime;
            if (mTargetPosition == leftSide)
            {
                mTargetPosition = rightSide;
            }
            else
            {
                mTargetPosition = leftSide;
            }
        }

        mCurrentAttackTimer--;

        if (mCurrentAttackTimer < chargeTime)
        {
            //TODO! Use a flag to check when this is reached for the first time.
            //Then toggle hitboxes on the boss so you can jump over it, and toggle back after the charge is over
            transform.position = mStartPosition +
                (mTargetPosition.position - mStartPosition) * (((float)chargeTime - (float)mCurrentAttackTimer) / (float)chargeTime);
        }
        else
        {
            if (mCurrentAttackTimer % 20 == 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, prepareJumpForce));
            }
        }

        if (mCurrentAttackTimer < 0)
        {
            mInAttack = false;
            PickNextAttack();
        }
    }

    void HandleAxeThrow()
    {
        if (mInAttack == false)
        {
            mInAttack = true;
            mCurrentAttackTimer = throwTime;
            mAxe = Instantiate(axe, mTargetPosition.position, Quaternion.identity);
            mAxeStartPosition = mAxe.transform.position;
            if (mTargetPosition == leftSide)
            {
                mAxeTargetPosition = rightSide;
            }
            else
            {
                mAxeTargetPosition = leftSide;
            }
        }

        mCurrentAttackTimer--;

        if (mCurrentAttackTimer > throwTime/2)
        {
            mAxe.transform.position = mAxeStartPosition +
                (mAxeTargetPosition.position - mAxeStartPosition) * ((((float)throwTime/2.0f) - ((float)mCurrentAttackTimer - ((float)throwTime / 2.0f))) / ((float)throwTime/2.0f));
        }
        else
        {
            mAxe.transform.position = mAxeTargetPosition.position +
                (mAxeStartPosition - mAxeTargetPosition.position) * ((((float)throwTime/2.0f) - (float)mCurrentAttackTimer) / ((float)throwTime/2.0f));
        }

        if (mCurrentAttackTimer < 0)
        {
            Destroy(mAxe);
            mAxe = null;
            mInAttack = false;
            PickNextAttack();
        }
    }

    void HandleBombing()
    {
        if (mInAttack == false)
        {
            mInAttack = true;
            mCurrentAttackTimer = bombTime;
        }

        if (mCurrentAttackTimer % 50 == 0)
        {
            Vector3 offset;
            int direct;
            if (mFacingRight)
            {
                offset = new Vector3(2, 0);
                direct = 1;
            }
            else
            {
                offset = new Vector3(-2, 0);
                direct = -1;
            }
            float y = Random.Range(-1f, 1f);
            Vector3 direction = new Vector3(direct, y).normalized;
            float angleInRadians = Mathf.Atan2(direction.y, direction.x);
            float angle = Mathf.Rad2Deg * angleInRadians;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            GameObject bomb = Instantiate(grenade, transform.position + offset, rot);
            float strength = Random.Range(0f, throwStrength);
            Rigidbody2D rbody = bomb.transform.GetChild(0).GetComponent<Rigidbody2D>();
            rbody.velocity = bomb.transform.rotation * (strength * new Vector3(1, 0, 0));
        }

        mCurrentAttackTimer--;

        if (mCurrentAttackTimer < 0)
        {
            mInAttack = false;
            PickNextAttack();
        }
    }

    void HandleSpreadFire()
    {
        if (mInAttack == false)
        {
            mInAttack = true;
            mCurrentAttackTimer = fireTime;
            mCurrentFlame = 0;
            mFireStartPosition = mTargetPosition.position;
            if (mTargetPosition == leftSide)
            {
                mFireTargetPosition = rightSide;
            }
            else
            {
                mFireTargetPosition = leftSide;
            }
        }

        if (mCurrentAttackTimer % (fireTime/(numberOfFlames + 2)) == 0)
        {
            Vector3 yOffset = Vector3.down * 1.0f;
            Vector3 pos = mFireStartPosition + (mFireTargetPosition.position - mFireStartPosition) * ((float)mCurrentFlame/(float)numberOfFlames);
            Instantiate(fire, pos + yOffset, Quaternion.identity);
            mCurrentFlame++;
        }

        mCurrentAttackTimer--;

        if (mCurrentAttackTimer < 0)
        {
            mInAttack = false;
            PickNextAttack();
        }
    }

    void HandleSwinging()
    {
        if (mInAttack == false)
        {
            mInAttack = true;
            mCurrentAttackTimer = swingTime;
            mAxeSwing = Instantiate(swing, transform);
        }

        mCurrentAttackTimer--;
        if (mInSecondPhase)
        {
            if (mFacingRight)
                transform.position = transform.position + Vector3.right / 7;
            else
                transform.position = transform.position + Vector3.left / 7;
        }
        else
        {
            if (mFacingRight)
                transform.position = transform.position + Vector3.right / 10;
            else
                transform.position = transform.position + Vector3.left / 10;
        }

        if (mCurrentAttackTimer < 0)
        {
            Destroy(mAxeSwing); 
            mAxeSwing = null;
            mInAttack = false;
            PickNextAttack();
        }
    }

    void HandleMovement()
    {
        if (mIsMoving == false)
        {
            mCurrentAttackTimer = mMovementTime;
            mIsMoving = true;
            //Disable collider for movement
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<BoxCollider2D>().enabled = false;
            mStartPosition = transform.position;
            mTargetPosition = FindTargetPosition();

        }

        transform.position = mStartPosition+ 
            (mTargetPosition.position - mStartPosition) * (((float)mMovementTime - (float)mCurrentAttackTimer)/(float)mMovementTime);

        mCurrentAttackTimer--;

        if (mCurrentAttackTimer < 0)
        {
            mIsMoving = false;
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Collider2D>().enabled = true;
            GetComponentInChildren<BoxCollider2D>().enabled = true;
            mCurrentState = mNextState;
        }
    }

    private Transform FindTargetPosition()
    {
        if (mNextState == states.trapping || mNextState == states.bombing)
        {
            if (Random.Range(0,2) == 0)
            {
                //Use target position, as this will contain the previous target position
                if (mTargetPosition.position != platform1.position)
                    return platform1;
                else
                    return platform2;
            }
            else
            {
                if (mTargetPosition.position != platform2.position)
                    return platform2;
                else
                    return platform1;
            }
        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                if (mTargetPosition.position != leftSide.position)
                    return leftSide;
                else
                    return rightSide;
            }
            else
            {
                if (mTargetPosition.position != rightSide.position)
                    return rightSide;
                else
                    return leftSide;
            }
        }
    }

    void FacePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (hit.collider == null)
            return;
        if (hit.collider.gameObject.tag == "Player")
        {
            //We've seen the player, make sure we're facing in the right direction
            if ((hit.transform.position.x < transform.position.x) && mFacingRight)
            {
                Flip();
            }
            else if ((hit.transform.position.x > transform.position.x) && !mFacingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        // Switch the way the boss is labelled as facing.
        mFacingRight = !mFacingRight;

        // Multiply the boss's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
