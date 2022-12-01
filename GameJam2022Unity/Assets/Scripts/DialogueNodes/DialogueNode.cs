using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueNode : ScriptableObject
{
}

public abstract class BranchDialogueNode : DialogueNode
{
    public abstract DialogueNode ChooseNode();
}