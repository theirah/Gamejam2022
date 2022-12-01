using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CorruptionBranch", menuName = "DialogueNode/CorruptionBranchDialogueNode", order = 1)]
public class CorruptionBranchDialogueNode : BranchDialogueNode
{
    public List<TextDialogueNode> possibleDialogues;
    public override DialogueNode ChooseNode()
    {
        if (possibleDialogues.Count == 0) return null;

        // Return appropriate node for current corruption level
        CorruptionStateManager corruptionStateManager = CorruptionStateManager.singleton;
        return possibleDialogues[corruptionStateManager.corruptionLevel];
    }
}
