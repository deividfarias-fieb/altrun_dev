using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LanternaController : MonoBehaviour
{
    public Light2D luzLanterna;
    public KeyCode teclaLanterna = KeyCode.R;    // Start is called before the first frame update
    public bool ligada = false;

    void Update()
    { 
        if (Input.GetKeyDown(teclaLanterna))
        {
            ligada = !ligada;

            luzLanterna.enabled = ligada;
        }
        
    }
}
