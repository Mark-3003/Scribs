using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Image image;
    public Text dialogueText;

    public GameObject textBox;

    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        textBox.SetActive(true);

        nameText.text = dialogue.name;
        image.sprite = dialogue.characterImg;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
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
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }
    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        textBox.SetActive(false);
    }
}
