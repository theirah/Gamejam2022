using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSpin : MonoBehaviour
{
    public int spinSpeed = 4;
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
            // Multiply the x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            count = 0;
        }
    }
}
