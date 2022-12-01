using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPos : MonoBehaviour
{
    public Vector3 pos;
    private bool hasMoved = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        if (!hasMoved)
        {
            hasMoved = true;
            transform.position = pos;
        }
    }
}
