using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Lever[] levers;        // arraste todas as alavancas aqui
    public GameObject door;       // porta que vai abrir
    public bool destroyDoor = true; // se vai destruir a porta ou só desativar

    public void CheckLevers()
    {
        foreach (Lever lever in levers)
        {
            if (!lever.isUp)
                return; // se alguma estiver pra baixo, não faz nada
        }

        // Se todas estão pra cima, abre a porta
        if (destroyDoor)
            Destroy(door);
        else
            door.SetActive(false);

        Debug.Log("Puzzle resolvido!");
    }

}
