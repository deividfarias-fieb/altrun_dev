using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class TriggerSalaTMP : MonoBehaviour
{
    [Header("Configuração da Mensagem")]
    public TMP_Text textoMensagem; 
    public string mensagem = "Preciso colocar alguma coisa na porta";
    public float tempoMensagem = 3f;

    [Header("Configuração do Temporizador")]
    public TMP_Text textoTimer; 
    public float tempoTotal = 10f; // tempo em segundos

    private bool jaAtivado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!jaAtivado && other.CompareTag("Player"))
        {
            jaAtivado = true; // garante que só executa uma vez
            StartCoroutine(SequenciaEvento());
        }
    }

    private IEnumerator SequenciaEvento()
    {
        // 1 - Mostrar a mensagem
        textoMensagem.text = mensagem;
        textoMensagem.gameObject.SetActive(true);
        yield return new WaitForSeconds(tempoMensagem);
        textoMensagem.gameObject.SetActive(false);

        // 2 - Iniciar o temporizador
        textoTimer.gameObject.SetActive(true);
        float tempoRestante = tempoTotal;

        while (tempoRestante > 0)
        {
            textoTimer.text = "Tempo: " + Mathf.CeilToInt(tempoRestante).ToString();
            tempoRestante -= Time.deltaTime;
            yield return null;
        }

        textoTimer.text = "Tempo esgotado!";
    }
}
