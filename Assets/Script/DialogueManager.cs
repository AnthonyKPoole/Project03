using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> DialogueSet;
    public Queue<string> ResponseSet;
    public TextMeshProUGUI Text_Name;
    public TextMeshProUGUI Text_Dialogue;
    public GameObject TextBox;
    public GameObject ChoiceBox;
    private state TextState = state.NotActive;
    public TextMeshProUGUI ChoiceButton1;
    public TextMeshProUGUI ChoiceButton2;
    public TextMeshProUGUI ChoiceButton3;
    void Start()
    {
        DialogueSet = new Queue<string>();
        ResponseSet = new Queue<string>();
    }
    private enum state
    {
        Active, NotActive
    }

    public void Begin(DialogueSet dialogue)
    {

        TextBox.SetActive(true);
        ChoiceBox.SetActive(false);
        DialogueSet.Clear();
        Text_Name.text = dialogue.Name;
        foreach (string sentence in dialogue.Dialogue)
        {
            DialogueSet.Enqueue(sentence);
        }
        DisplayNextPart();
        foreach (string sentence in dialogue.Response)
        {
            ResponseSet.Enqueue(sentence);
        }
    }
    public void DisplayNextPart()
    {
        if (DialogueSet.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = DialogueSet.Dequeue();
        StartCoroutine(TypeEffect(sentence));
    }
    //Allows for a typewriter effect on text.
    IEnumerator TypeEffect(string Text)
    {
        Text_Dialogue.text = "";
        TextState = state.Active;
        int WordIndex = 0;
        while (TextState != state.NotActive)
        {
            if(Text_Dialogue.text != "*")
            {
                Text_Dialogue.text += Text[WordIndex];
                yield return new WaitForSeconds(0.05f);
                if (++WordIndex == Text.Length)
                {
                    TextState = state.NotActive;
                    break;
                }
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (TextState == state.NotActive)
            {
                StopAllCoroutines();
                DisplayNextPart();
            }
            if (Text_Dialogue.text == "*")
            {
                TextBox.SetActive(false);
                ChoiceBox.SetActive(true);
                PlayerChoice();
            }
        }
        
    }
    public void EndDialogue()
    {
        TextBox.SetActive(false);
        ChoiceBox.SetActive(false);
        ActivateDialogue.ActiveText = false;
    }

    public void PlayerChoice()
    {

        ChoiceButton1.text = ResponseSet.Dequeue();
        ChoiceButton2.text = ResponseSet.Dequeue();
        ChoiceButton3.text = ResponseSet.Dequeue();
        if (ChoiceButton2.text == "#")
        {
            ChoiceButton2.text = " ";
        }

        if (ChoiceButton3.text == "#")
        {
            ChoiceButton3.text = " ";
        }
    }

}
