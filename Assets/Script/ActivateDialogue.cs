using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    static public bool ActiveText = false;
    public DialogueSet dialogue;

    public void TriggerDialogue()
    {
        if (ActiveText == false)
        {
            FindObjectOfType<DialogueManager>().Begin(dialogue);
            ActiveText = true;
        }
        
    }
}
