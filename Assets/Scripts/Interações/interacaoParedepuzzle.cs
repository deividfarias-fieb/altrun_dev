using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interacaoParedepuzzle : MonoBehaviour
{
     public GameObject aperteE; 
     public GameObject leverPuzzleUI; // Canvas com as alavancas
    private bool isNear = false;

    void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            aperteE.SetActive(false);
            leverPuzzleUI.SetActive(true);
            Time.timeScale = 0f; // pausa o jogo
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isNear = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isNear = false;
    }
}
