using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain_code : MonoBehaviour
{
    public GameObject raindrop;
    [SerializeField]
    private Rigidbody2D rig;
    [SerializeField]
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
