using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogoMouse : MonoBehaviour
{
   [TextArea(2,6)]
    public string[] dialogueLines;
    public GameObject ObjetoDesejado;
    public dialogoDelete dialogueDelete;
    public ativarItem ativarqueItem;
    public bool ativarItem;
    public bool ativarChave;
    public GameObject excluirObj;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // clique esquerdo do mouse
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = Physics2D.OverlapPoint(mousePos);

            if (col != null && col.gameObject == gameObject)
            {
                if (DialogueManager.Instance != null)
                {
                    DialogueManager.Instance.StartDialogue(dialogueLines);
                }

                ObjetoDesejado.SetActive(true);
                excluirObj.SetActive(false);

                if (ativarItem)
                {
                    dialogueDelete.jogadorTemItem = true;
                }

                if (ativarChave)
                {
                    ativarqueItem.chaveAtiva = true;
                }
            }
        }
    }
}
