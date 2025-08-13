using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class madeiraPorta : MonoBehaviour
{
    public GameObject objetoParaAtivar; // O que vai aparecer ao interagir
    public GameObject objetoParaDeletar; // O que foi ativado antes e precisa sumir
    public KeyCode teclaInteracao = KeyCode.E;

    private bool podeInteragir = false;

    void Update()
    {
        if (podeInteragir && Input.GetKeyDown(teclaInteracao))
        {
            if (objetoParaAtivar != null)
                objetoParaAtivar.SetActive(true);

            if (objetoParaDeletar != null)
                Destroy(objetoParaDeletar);

            Destroy(gameObject); // Deleta este objeto
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            podeInteragir = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            podeInteragir = false;
    }
}
