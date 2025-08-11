using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyPad : MonoBehaviour
{
    [SerializeField] private Text Ans;
    public GameObject portaabrir;

    private string Answer = "5671";
    public void Number(int number){
        Ans.text += number.ToString();
    }

    public void Execute(){
        if(Ans.text == Answer){
            portaabrir.SetActive(true);
            Ans.text = "Correct";
            
            
        }else{
            Ans.text = "Incorrect";
            StartCoroutine(LimparDepoisDeTempo(2f));
        }

        
    }

    public void Clear(){
        Ans.text = "";
    }

    private IEnumerator
    LimparDepoisDeTempo(float tempo){

        yield return new WaitForSecondsRealtime(tempo);
        Ans.text = "";
    }
}
