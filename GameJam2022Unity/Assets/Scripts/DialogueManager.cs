using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;
    public Button continueButton;
    public Button choiceAButton;
    public Button choiceBButton;

    protected TextDialogueNode mCurrentDialogueNode;
    private Queue<DialogueSentence> sentences = new Queue<DialogueSentence>();
    protected UnityEvent onAllDialoguesFinishedEvent;

    protected DialogueOption mChosenOption;


    public void StartDialogues(DialogueNode rootDialogueNode, UnityEvent allDialoguesFinishedEvent)
    {
        FindObjectOfType<PauseManager>().PauseAll();
        onAllDialoguesFinishedEvent = allDialoguesFinishedEvent;

        if (!(rootDialogueNode is TextDialogueNode))
        {
            rootDialogueNode = TraverseToNextText(rootDialogueNode);
        }

        mCurrentDialogueNode = rootDialogueNode as TextDialogueNode;
        StartDialogue(mCurrentDialogueNode.dialogue);
    }

    protected TextDialogueNode TraverseToNextText(DialogueNode fromNode)
    {
        if (fromNode is SimpleDialogueNode)
        {
            return null;
        }
        if (fromNode is BranchDialogueNode)
        {
            DialogueNode nextNode = (fromNode as BranchDialogueNode).ChooseNode();
            if (nextNode == null) return null;
            if (nextNode is TextDialogueNode) return nextNode as TextDialogueNode;
            else return TraverseToNextText(nextNode);
        }
        if (mCurrentDialogueNode is ChoiceDialogueNode)
        {
            DialogueNode nextNode = mChosenOption.nextNode;
            mChosenOption = null;
            if (nextNode == null) return null;
            if (nextNode is TextDialogueNode) return nextNode as TextDialogueNode;
            else return TraverseToNextText(nextNode);
        }
        return null;
    }

    protected void TryStartNextDialogue()
    {
        TextDialogueNode nextNode = TraverseToNextText(mCurrentDialogueNode);

        if (nextNode)
        {
            mCurrentDialogueNode = nextNode;
            StartDialogue(mCurrentDialogueNode.dialogue);
        }
        else
        {
            EndAllDialogues();
        }
    }

    protected void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);


        sentences.Clear();

        foreach (DialogueSentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (mCurrentDialogueNode is SimpleDialogueNode || sentences.Count > 0)
        {
            continueButton.gameObject.SetActive(true);
            choiceAButton.gameObject.SetActive(false);
            choiceBButton.gameObject.SetActive(false);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
        DialogueSentence sentence = sentences.Dequeue();
        nameText.text = sentence.speaker;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        if (mCurrentDialogueNode is ChoiceDialogueNode && sentences.Count == 0)
        {
            ChoiceDialogueNode choiceNode = mCurrentDialogueNode as ChoiceDialogueNode;
            TextMeshProUGUI choiceAText = choiceAButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceAText.text = choiceNode.choiceA.buttonText;
            choiceAButton.gameObject.SetActive(true);
            TextMeshProUGUI choiceBText = choiceBButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceBText.text = choiceNode.choiceB.buttonText;
            choiceBButton.gameObject.SetActive(true);
        }
    }

    public void ChooseA()
    {
        ChoiceDialogueNode choiceNode = mCurrentDialogueNode as ChoiceDialogueNode;
        mChosenOption = choiceNode.choiceA;
        mChosenOption.ExecuteChoice();
        EndDialogue();
    }

    public void ChooseB()
    {
        ChoiceDialogueNode choiceNode = mCurrentDialogueNode as ChoiceDialogueNode;
        mChosenOption = choiceNode.choiceB;
        EndDialogue();
    }


    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        TryStartNextDialogue();
    }

    void EndAllDialogues()
    {
        FindObjectOfType<PauseManager>().UnpauseAll();
        onAllDialoguesFinishedEvent.Invoke();
    }
}
