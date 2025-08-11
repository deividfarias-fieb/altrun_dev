using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Controller : MonoBehaviour
{
    [SerializeField] private Animator animatorPanel;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonExit;
    private readonly string openParam = "openPanel";

    public void OpenPanel()
    {
        if (animatorPanel != null)
        {
            animatorPanel.SetBool(openParam, true);
            buttonPlay.interactable = false;
            buttonCredits.interactable = false;
            buttonExit.interactable=false;
        }
    }

    public void ClosePanel()
    {
        if (animatorPanel != null)
        {
            animatorPanel.SetBool(openParam, false);
            buttonPlay.interactable = true;
            buttonCredits.interactable = true;
            buttonExit.interactable = true;
        }
    }

    public void TogglePanel()
    {
        if (animatorPanel != null)
        {
            bool isOpen = animatorPanel.GetBool(openParam);
            animatorPanel.SetBool(openParam, !isOpen);
        }
    }
}
