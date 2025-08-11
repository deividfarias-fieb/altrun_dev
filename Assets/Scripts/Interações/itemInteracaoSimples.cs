using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteracaoSimples : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger; // Arrasta o objeto que tem DialogueTrigger aqui
    public KeyCode teclaInteracao = KeyCode.F;
    public alcapao2 alcapao;

    private bool playerPerto = false;
    

    public GameObject icone;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerPerto = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerPerto = false;
    }

    void Update()
    {
        if (playerPerto && Input.GetKeyDown(teclaInteracao))
        {
            // Faz a ação: exemplo, marca que o jogador tem o item
            
            // Some com o item
            Destroy(gameObject);
            icone.SetActive(true);
            alcapao.jogadorTemChave= true;
        }
    }
}
