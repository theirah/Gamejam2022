using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public GameObject player;
    //Distance from the left of the screen that the camera will start moving
    public float leftMargin = 0.2f;
    //Distance from the right of the screen that the camera will start moving
    public float rightMargin = 0.2f;
    //Distance from the top of the screen that the camera will start moving
    public float topMargin = 0.2f;
    //Distance from the bottom of the screen that the camera will start moving
    public float bottomMargin = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = Camera.main.WorldToViewportPoint(player.transform.position);
        if (playerPos.x < leftMargin)
        {
            transform.position += player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(leftMargin, playerPos.y, playerPos.z));
        }
        else if (playerPos.x > (1 - rightMargin))
        {
            transform.position += player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(1 - rightMargin, playerPos.y, playerPos.z));
        }

        if (playerPos.y < bottomMargin)
        {
            transform.position += player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(playerPos.x, bottomMargin, playerPos.z));
        }
        else if (playerPos.y > (1 - topMargin))
        {
            transform.position += player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(playerPos.x, 1 - topMargin, playerPos.z));
        }
    }
}
