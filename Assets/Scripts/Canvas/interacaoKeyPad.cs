using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interacaoKeyPad : MonoBehaviour
{
    public GameObject pinPad;
    public MonoBehaviour playerMovementScript;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            pinPad.SetActive(true);
            if (playerMovementScript != null) playerMovementScript.enabled = false;
            Debug.Log("Interagiu com " + gameObject.name);
        }
    }

    public void Sair(){
        pinPad.SetActive(false);
        if (playerMovementScript != null) playerMovementScript.enabled = true;
    }
}
