using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
   public static DialogueManager Instance;

    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public float typingSpeed = 0.03f;
    public MonoBehaviour playerMovementScript;

    private string[] lines;
    private int index;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private bool isOpen;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        dialogPanel.SetActive(false);

        // TESTE: Chamando diálogo automaticamente

       
    }

    void Update()
    {
        if (!isOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tecla E pressionada");

            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogText.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    public void StartDialogue(string[] dialogueLines)
    {
        lines = dialogueLines;
        index = 0;
        dialogPanel.SetActive(true);
        isOpen = true;
        if (playerMovementScript != null) playerMovementScript.enabled = false;

        typingCoroutine = StartCoroutine(TypeLine(lines[index]));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char c in line)
        {
            dialogText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            typingCoroutine = StartCoroutine(TypeLine(lines[index]));
        }
        else
        {
            Debug.Log("Fim do diálogo");
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogPanel.SetActive(false);
        isOpen = false;
        if (playerMovementScript != null) playerMovementScript.enabled = true;
    }
}
