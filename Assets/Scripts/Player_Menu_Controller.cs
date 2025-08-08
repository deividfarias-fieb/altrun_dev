using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player_Menu_Controller : MonoBehaviour
{
    // Mover o player em ambas as direcoes
    [SerializeField] private SliderJoint2D sliderPlayer; //O [SerializeField] ajuda a otimizar o jogo e cumpre a mesma fun��o que o public
    [SerializeField] private JointMotor2D motorTranslation;
    [SerializeField] private Animator anim;
    [SerializeField] private JointTranslationLimits2D newLimits = new JointTranslationLimits2D();
    [SerializeField] private JointTranslationLimits2D oldLimits = new JointTranslationLimits2D();

    private bool isMovingRight = true; 
    private bool isPaused = false;

    [SerializeField] private TextMeshProUGUI interactionText;

    // Start is called before the first frame update
    void Start()
    {
        motorTranslation = sliderPlayer.motor;
        oldLimits = sliderPlayer.limits;
        StartCoroutine(MovePlayer());

        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    IEnumerator MovePlayer()
    {
        while (true) 
        {
            if (!isPaused) 
            {
                newLimits.min = 0f;
                newLimits.max = 15f;

                // Rotate Y = 0 > Running to right screen.
                if (sliderPlayer.limitState == JointLimitState2D.LowerLimit && isMovingRight)
                {
                    isMovingRight = false;
                    isPaused = true; // Pausa o movimento
                    anim.SetBool("isRight", false);
                    anim.SetBool("isDuck", true);
                    yield return new WaitForSeconds(2f); // Espera 2 segundos
                    isPaused = false; // Retoma o movimento
                    transform.Rotate(0, 180, 0);
                    sliderPlayer.limits = newLimits;
                }
                else if (sliderPlayer.limitState == JointLimitState2D.UpperLimit && !isMovingRight)
                {
                    isMovingRight = true;
                    isPaused = true; // Pausa o movimento
                    anim.SetBool("isRight", false);
                    anim.SetBool("isDuck", true);
                    transform.Rotate(0, -180, 0);
                    sliderPlayer.limits = oldLimits;
                    yield return new WaitForSeconds(2f); // Espera 2 segundos
                    isPaused = false; // Retoma o movimento
                }

                motorTranslation.motorSpeed = isMovingRight ? -1.5f : 1.5f;
                anim.SetBool("isRight", true);
                anim.SetBool("isDuck", false);
                sliderPlayer.motor = motorTranslation;
            }
            yield return null; // Espera um frame antes de continuar o loop
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("OnTriggerEnter2D chamado! Objeto colidido: " + other.gameObject.name + " (Tag: " + other.tag + ")");
        if (other.gameObject.CompareTag("Button"))
        {
            //Debug.Log("Colisao com coletavel detectada!");
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
                //interactionText.text = "Pressione E para interagir!";
            }
            // Opcional: Destroi o objeto colidido
            // Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Button"))
        {
            //Debug.Log("Saiu da colisao com coletavel.");
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }
}
