using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public GameObject player;
    //Distance from the edges of the screen that the camera will start moving
    public float xMargin = 0.2f;
    //Distance from the top and bottom of the screen that the camera will start moving
    public float yMargin = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = Camera.main.WorldToViewportPoint(player.transform.position);
        if (playerPos.x < xMargin)
        {
            transform.position += player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(xMargin, playerPos.y, playerPos.z));
        }
        else if (playerPos.x > (1 - xMargin))
        {
            transform.position += player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(1 - xMargin, playerPos.y, playerPos.z));
        }

        if (playerPos.y < yMargin)
        {
            transform.position += player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(playerPos.x, yMargin, playerPos.z));
        }
        else if (playerPos.y > (1 - yMargin))
        {
            transform.position += player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(playerPos.x, 1 - yMargin, playerPos.z));
        }
    }
}
