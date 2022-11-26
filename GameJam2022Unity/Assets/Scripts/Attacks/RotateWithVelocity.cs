using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithVelocity : MonoBehaviour
{
    [SerializeField]
    bool disableOnCollision;

    bool isDisabled;

    private void Start()
    {
        isDisabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (!isDisabled && rigidbody)
        {
                this.transform.rotation = Quaternion.Euler(0, 0, DirectionToAngle(rigidbody.velocity));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (disableOnCollision) isDisabled = true;
    }

    protected float DirectionToAngle(Vector3 direction)
    {
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);
        return Mathf.Rad2Deg * angleInRadians;
    }
}
