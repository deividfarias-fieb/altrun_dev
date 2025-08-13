using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desativarTimer : MonoBehaviour
{
     public GameObject canvasTimer;  // Arraste aqui o Canvas do timer
    public GameObject timer;        // Arraste aqui o objeto que contém o script timer ou o timer em s

    void OnEnable()
    {
        if (canvasTimer != null)
            canvasTimer.SetActive(false);

        if (timer != null)
            timer.SetActive(false);
    }
}
