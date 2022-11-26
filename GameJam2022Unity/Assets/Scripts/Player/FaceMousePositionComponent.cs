using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMousePositionComponent : MonoBehaviour
{

    public bool m_FacingRight = true;  // For determining which way the player is currently facing.

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; // distance between camera and grid, whose position is at 0
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        bool shouldFaceRight = mouseWorldPos.x >= transform.position.x;
        if ((shouldFaceRight && !m_FacingRight) || (!shouldFaceRight && m_FacingRight)) Flip();
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
