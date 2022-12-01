using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueSentence
{
    public string speaker;

    [TextArea(3, 10)]
    public string sentence;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueSentence> sentences;
}