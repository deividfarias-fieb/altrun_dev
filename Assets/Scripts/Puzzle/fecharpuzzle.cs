using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fecharpuzzle : MonoBehaviour
{
    public GameObject puzzleSpr;
    public GameObject fecharE;

    void OnMouseDown()
    {   
        fecharE.SetActive(true);
        puzzleSpr.SetActive(false);
        Time.timeScale = 1f;
    }
}
