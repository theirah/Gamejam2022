using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public List<Dialogue> dialogues;
    public UnityEvent onDialogueEnd;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogues(dialogues, onDialogueEnd);
    }
}
