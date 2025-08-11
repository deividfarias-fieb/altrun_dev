using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(2,6)]
    public string[] dialogueLines;
    public GameObject interactPrompt;
    public GameObject ObjetoDesejado;
    public dialogoElse dialogueElse;
    public ativarItem ativarqueItem;
    public bool ativarItem;
    public bool ativarChave;
    

    private bool playerInRange = false;

    void Start()
    {
        if (interactPrompt != null) interactPrompt.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactPrompt != null) interactPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactPrompt != null) interactPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (interactPrompt != null) interactPrompt.SetActive(false);
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogue(dialogueLines);
            }
            ObjetoDesejado.SetActive(true);
            if (ativarItem == true){
            dialogueElse.jogadorTemItem = true;
            }
            if(ativarChave == true){
                ativarqueItem.chaveAtiva = true;
            }
        }
    }
        
}
