using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raindrop_Code : MonoBehaviour
{
    public GameObject raindrop;
    public Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
