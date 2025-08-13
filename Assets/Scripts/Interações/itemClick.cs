using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemClick : MonoBehaviour
{
    public GameObject nextItem; // item que vai aparecer depois
    public float delay = 0.1f;  // tempo até ativar o próximo item

    void OnMouseDown()
    {
        gameObject.SetActive(false); // desativa o item atual
        if (nextItem != null)
            Invoke(nameof(ActivateNext), delay);
    }

    void ActivateNext()
    {
        nextItem.SetActive(true); // ativa o próximo item
        
    }
}
