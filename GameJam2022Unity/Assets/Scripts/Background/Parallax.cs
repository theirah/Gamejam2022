using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public GameObject mainCamera;
    public float parallaxAmount;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = (mainCamera.transform.position.x * parallaxAmount);
        float edgePos = (mainCamera.transform.position.x * (1 - parallaxAmount));
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (edgePos > startPos + length/2)
            startPos += length;
        else if (edgePos < startPos - length/2)
            startPos -= length;
    }
}
