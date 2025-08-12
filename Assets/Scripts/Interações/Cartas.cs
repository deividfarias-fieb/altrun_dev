using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Cartas : MonoBehaviour
{
   public GameObject Carta;
 
   public void OnTriggerEnter2D(Collider2D other)
   {
    if (Input.GetKeyDown(KeyCode.F)){
    Carta.SetActive(true);
    
    }
   }
}