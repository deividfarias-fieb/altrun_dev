using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Controller : MonoBehaviour
{
    [SerializeField] private GameObject mouseSprite;
    // Start is called before the first frame update
    void Start()
    {
        mouseSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OpenEgg()
    {
        StartCoroutine(EnableEasterEgg());
    }

    IEnumerator EnableEasterEgg()
    {
        mouseSprite.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1.9f);
        mouseSprite.GetComponent<SpriteRenderer>().enabled = false;
    }
}
