using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventario : MonoBehaviour
{
    public Animator animator;

    private bool isOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            isOpen = !isOpen;
            animator.SetBool("IsOpen", isOpen);
        }
    }
}
