using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWithRandomYRotation : MonoBehaviour
{
    public GameObject[] targets;
    public Vector3 scale;
    public int leftRot;
    public int rightRot;
    // Start is called before the first frame update
    void Start()
    {
        GameObject target = targets[Random.Range(0, targets.Length)];
        Quaternion randRot = Quaternion.Euler(new Vector3(0, Random.Range(-leftRot, rightRot)));
        GameObject instance = Instantiate(target, transform.position, randRot);
        instance.transform.parent = transform;
        //Scale up this transform, not the instance, so the rotation isn't applied
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
