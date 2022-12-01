using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActivePlayerBranch", menuName = "DialogueNode/ActivePlayerBranchDialogue", order = 1)]
public class ActivePlayerBranchDialogueNode : BranchDialogueNode
{
    public List<DialogueNode> dialogueNodesBySwapCharacterIndex = new List<DialogueNode>();
    public override DialogueNode ChooseNode()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SwapCharacterComponent swapComponent = player.transform.parent.GetComponent<SwapCharacterComponent>();
        if (swapComponent)
        {
            return dialogueNodesBySwapCharacterIndex[swapComponent.mCurrIndex];
        }
        return null;
    }
}
