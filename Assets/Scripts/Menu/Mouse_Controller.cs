using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class Mouse_Controller : MonoBehaviour
{
    [SerializeField] private GameObject mouseSprite;
    [SerializeField] private GameObject lightMouse;
    // Start is called before the first frame update
    void Start()
    {
        mouseSprite.GetComponent<SpriteRenderer>().enabled = false;
        lightMouse = GameObject.FindGameObjectWithTag("Mouse_Egg");
        lightMouse.GetComponent<Light2D>().intensity = 0f;
    }

    public void OpenEgg()
    {
        StartCoroutine(EnableEasterEgg());
    }

    IEnumerator EnableEasterEgg()
    {
        lightMouse.GetComponent<Light2D>().intensity = 1.5f;
        mouseSprite.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1.9f);
        mouseSprite.GetComponent<SpriteRenderer>().enabled = false;
        lightMouse.GetComponent<Light2D>().intensity = 0f;
    }
}
