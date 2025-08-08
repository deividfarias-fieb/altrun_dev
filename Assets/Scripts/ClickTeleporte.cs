using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTeleporte : MonoBehaviour
{
    public GameObject player; // Arraste o Player no Inspector
    public KeyCode teclaInteracao = KeyCode.E; // Tecla para teleporte
    private PortaTeleporte portaAtual; // Porta com a qual está colidindo

    void Update()
    {
        if (portaAtual != null && Input.GetKeyDown(teclaInteracao))
        {
            player.transform.position = portaAtual.destino.position;
            Debug.Log("Teleportado para " + portaAtual.destino.name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Porta"))
        {
            portaAtual = collision.collider.GetComponent<PortaTeleporte>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Porta"))
        {
            portaAtual = null;
        }
    }
}