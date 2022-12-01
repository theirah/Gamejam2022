using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPos : MonoBehaviour
{
    public Vector3 pos;
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
        transform.position = pos;
    }
}
