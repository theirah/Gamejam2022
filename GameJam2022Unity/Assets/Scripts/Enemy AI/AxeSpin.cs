using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSpin : MonoBehaviour
{
    public int spinSpeed = 4;
    public bool isY = false;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        count++;
        if (count > spinSpeed)
        {
            // Multiply the local scale by -1.
            Vector3 theScale = transform.localScale;
            if (isY )
                theScale.y *= -1;
            else 
                theScale.x *= -1;
            transform.localScale = theScale;
            count = 0;
        }
    }
}
