using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
