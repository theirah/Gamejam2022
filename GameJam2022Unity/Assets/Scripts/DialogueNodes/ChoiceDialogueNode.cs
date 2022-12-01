using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public enum Action { NONE, RAISE_CORRUPTION };

    public string buttonText;
    public DialogueNode nextNode;
    [SerializeField]
    protected Action consequences;
    public void ExecuteChoice()
    {
        if (consequences == Action.RAISE_CORRUPTION)
        {
            CorruptionStateManager.singleton.corruptionLevel++;
        }
    }
}

[CreateAssetMenu(fileName = "ChoiceDialogue", menuName = "DialogueNode/ChoiceDialogueNode", order = 2)]
public class ChoiceDialogueNode : TextDialogueNode
{
    public DialogueOption choiceA;
    public DialogueOption choiceB;
}