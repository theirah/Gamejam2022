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

    protected Queue<Dialogue> mDialogues = new Queue<Dialogue>();
    private Queue<string> sentences = new Queue<string>();
    protected UnityEvent onAllDialoguesFinishedEvent;

    public void StartDialogues(List<Dialogue> dialogues, UnityEvent allDialoguesFinishedEvent)
    {
        foreach (Dialogue dialogue in dialogues)
        {
            mDialogues.Enqueue(dialogue);
        }
        onAllDialoguesFinishedEvent = allDialoguesFinishedEvent;
        TryStartNextDialogue();
    }

    protected void TryStartNextDialogue()
    {
        if (mDialogues.Count > 0)
        {
            Dialogue currDialogue = mDialogues.Dequeue();
            StartDialogue(currDialogue);
        }
        else
        {
            EndAllDialogues();
        }
    }

    protected void StartDialogue (Dialogue dialogue)
    {
        FindObjectOfType<PauseManager>().PauseAll();
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
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

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
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
