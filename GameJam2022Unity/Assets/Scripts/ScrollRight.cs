using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRight : MonoBehaviour
{
    public float scrollAmount = 1f;
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
        transform.position = transform.position + new Vector3(scrollAmount, 0);
    }
}
