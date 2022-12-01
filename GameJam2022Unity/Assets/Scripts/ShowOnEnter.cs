using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnEnter : MonoBehaviour
{
    public GameObject toShow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            toShow.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
