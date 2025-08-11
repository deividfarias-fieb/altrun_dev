using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ativarItem : MonoBehaviour
{
    public bool chaveAtiva = false;
    public GameObject chave;
    // Update is called once per frame
    void Update()
    {
        if (chaveAtiva == true){
            chave.SetActive(true);
            
        }
    }
}
