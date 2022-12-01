using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerOnEnterTrigger : DialogueTrigger
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            TriggerDialogue();
        }
    }
}
