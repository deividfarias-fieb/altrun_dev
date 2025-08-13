using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class madeira : MonoBehaviour
{
     public GameObject objetoParaAtivar; // O objeto que vai aparecer quando pegar a madeira

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objetoParaAtivar != null)
                objetoParaAtivar.SetActive(true);

            Destroy(gameObject); // Some a madeira
        }
    }
}
