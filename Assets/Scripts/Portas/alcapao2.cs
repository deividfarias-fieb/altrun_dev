using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alcapao2 : MonoBehaviour
{
    [TextArea(2, 6)]
    public string[] dialogueComChave; // Fala quando o jogador TEM a chave
    public string dialogueSemChave = "Você precisa da chave para abrir isso!"; // Fala quando NÃO tem

    public GameObject interactPrompt; // Texto de "Pressione F"
    public GameObject objetoParaRemover; // O que vai sumir (o alçapão)
    public GameObject objetoParaAtivar;  // O que vai aparecer (o buraco)

    public bool jogadorTemChave = false; // Vai ser controlado por outro script quando pegar a chave

    private bool playerInRange = false;

    void Start()
    {
        if (interactPrompt != null) interactPrompt.SetActive(false);
        if (objetoParaAtivar != null) objetoParaAtivar.SetActive(false);
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
                if (jogadorTemChave)
                {
                    // Fala o diálogo correto
                    DialogueManager.Instance.StartDialogue(dialogueComChave);

                    // Remove o objeto antigo e ativa o novo
                    if (objetoParaRemover != null) Destroy(objetoParaRemover);
                    if (objetoParaAtivar != null) objetoParaAtivar.SetActive(true);
                }
                else
                {
                    // Fala que precisa da chave
                    DialogueManager.Instance.StartDialogue(new string[] { dialogueSemChave });
                }
            }
        }
    }
}
