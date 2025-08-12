using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{   
    public float tempoInicial = 60f;
    private float tempoAtual;

    public Text textoTimer;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        tempoAtual = tempoInicial;
    }

    // Update is called once per frame
    void Update()
    {
        if (tempoAtual > 0)
        {
            tempoAtual -= Time.deltaTime;
            AtualizarTexto();
        }else{
            tempoAtual = 0;
            acabouOTempo();
        }
    }
    void AtualizarTexto()
    {
        int minutos = Mathf.FloorToInt(tempoAtual / 60);
        int segundos = Mathf.FloorToInt(tempoAtual % 60);
        textoTimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
    void acabouOTempo(){
        Destroy(player);
        Debug.Log("Tempo Acabou");
    }
}
