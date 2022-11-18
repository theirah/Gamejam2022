using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : MonoBehaviour
{
    public int attackDistance = 3;

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
        //Check if the enemy can see the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (hit.collider.gameObject.tag == "Player")
        {
            if (Vector3.Magnitude(hit.transform.position - transform.position) < attackDistance)
            {
                Debug.Log("Pig attacking!");
            }
        }
        else
        {
            Debug.Log("Pig can't see player");
        }
    }
}
