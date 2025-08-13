using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogoDelete : MonoBehaviour
{
    [TextArea(2, 6)]
    public string[] dialogueLines; // Diálogo pra quando o jogador tem o item
    public string faltaItemMensagem = "Você precisa do item X pra isso!"; // Frase fixa se não tiver o item

    public GameObject interactPrompt;
    public GameObject objetoDesejado;
    public GameObject iconeObjeto;
    public GameObject objetoExcluir;
    public GameObject itemParaDeletar; // <-- Item que será deletado da cena

    public bool jogadorTemItem = false; // Controle se o jogador tem o item ou não

    private bool playerInRange = false;

    void Start()
    {
        if (interactPrompt != null) interactPrompt.SetActive(false);
        if (objetoDesejado != null) objetoDesejado.SetActive(false);
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
                if (jogadorTemItem)
                {
                    // Se o jogador tem o item
                    DialogueManager.Instance.StartDialogue(dialogueLines);

                    if (objetoDesejado != null) objetoDesejado.SetActive(true);
                    if (iconeObjeto != null) iconeObjeto.SetActive(true);
                    if (objetoExcluir != null) objetoExcluir.SetActive(false);

                    // Deletar o item da cena
                    if (itemParaDeletar != null)
                        Destroy(itemParaDeletar);
                }
                else
                {
                    // Se não tem, só avisa
                    DialogueManager.Instance.StartDialogue(new string[] { faltaItemMensagem });
                }
            }
        }
    }
}
