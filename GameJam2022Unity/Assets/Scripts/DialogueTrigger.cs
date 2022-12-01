using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueNode rootDialogueNode;
    public UnityEvent onDialogueEnd;

    public void TriggerDialogue()
    {
        FindObjectOfType<AudioManager>().StageEnd();
        FindObjectOfType<DialogueManager>().StartDialogues(rootDialogueNode, onDialogueEnd);
    }
}
