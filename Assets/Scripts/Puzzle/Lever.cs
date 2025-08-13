using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    public bool isUp = false; // estado da alavanca
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

    void OnMouseDown()
    {
        // Esse método é chamado automaticamente ao clicar no Collider do objeto
        isUp = !isUp;
        UpdateSprite();
        puzzleManager.CheckLevers();
    }

    void UpdateSprite()
    {
        sr.sprite = isUp ? upSprite : downSprite;
    }
}
