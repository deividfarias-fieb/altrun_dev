using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
  public bool IsUp = false;
  public Sprite upSprite;
  public Sprite downSprite;

  private SpriteRenderer sr;
  private PuzzleManager puzzleManager;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
        UpdateSprite();      
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            IsUp = !IsUp;

            UpdateSprite();
            puzzleManager.CheckLevers();
        }
    }

   void UpdateSprite()
   {
    sr.sprite = IsUp ? upSprite : downSprite;
   }
}
