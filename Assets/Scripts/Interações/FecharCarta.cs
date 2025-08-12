using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class FecharCarta : MonoBehaviour
{
    public void OnMouseDown()
    {
        Destroy(gameObject);
    }
}