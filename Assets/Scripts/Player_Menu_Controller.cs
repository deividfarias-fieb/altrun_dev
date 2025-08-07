using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player_Menu_Controller : MonoBehaviour
{
    // Mover o player em ambas as direcoes
    [SerializeField] private SliderJoint2D sliderPlayer; //O [SerializeField] ajuda a otimizar o jogo e cumpre a mesma fun��o que o public
    [SerializeField] private JointMotor2D motorTranslation;
    [SerializeField] private Animator anim;
    [SerializeField] private string lanternTag = "Lantern_Light";

    private bool isMovingRight = true; 
    private bool isPaused = false;

    [SerializeField] private TextMeshProUGUI interactionText;

    // Start is called before the first frame update
    void Start()
    {
        motorTranslation = sliderPlayer.motor;
        StartCoroutine(MovePlayer());

        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    IEnumerator MovePlayer()
    {
        // FAZER EM ARRAY OBJ[0],[1]
        GameObject lanternRight = GameObject.FindGameObjectWithTag("Lantern_Right");
        GameObject lanternLeft = GameObject.FindGameObjectWithTag("Lantern_Left");

        Light2D lightRight= lanternRight.GetComponent<Light2D>();
        Light2D lightLeft = lanternLeft.GetComponent<Light2D>();


        while (true) // Loop infinito para o movimento cont�nuo
        {
            if (!isPaused) // S� move se n�o estiver pausado
            {
                // Verifica o limite e inverte a dire��o
                if (sliderPlayer.limitState == JointLimitState2D.LowerLimit && isMovingRight)
                {
                    isMovingRight = false;
                    isPaused = true; // Pausa o movimento
                    anim.SetBool("isRight", false); // Garante que a anima��o anterior seja desativada
                    anim.SetBool("isLeft", false);  // Garante que a anima��o anterior seja desativada
                    anim.SetBool("isDuck", true); // Ou defina uma anima��o de idle/parado
                    Debug.Log("Chegou no limite superior. Pausando...");
                    yield return new WaitForSeconds(2f); // Espera 2 segundos
                    lightRight.intensity = (lightRight.intensity == 6 && isPaused) ? 0 : 6;
                    Debug.Log("Retomando movimento para a esquerda.");
                    isPaused = false; // Retoma o movimento
                    lightLeft.intensity = lightLeft.intensity <= 0 ? 6 : 0;
                }
                else if (sliderPlayer.limitState == JointLimitState2D.UpperLimit && !isMovingRight)
                {
                    isMovingRight = true;
                    isPaused = true; // Pausa o movimento
                    anim.SetBool("isRight", false); // Garante que a anima��o anterior seja desativada
                    anim.SetBool("isLeft", false);  // Garante que a anima��o anterior seja desativada
                    anim.SetBool("isDuck", true); // Ou defina uma anima��o de idle/parado
                    Debug.Log("Chegou no limite inferior. Pausando...");
                    yield return new WaitForSeconds(2f); // Espera 2 segundos
                    lightLeft.intensity = (lightLeft.intensity == 6 && isPaused) ? 0 : 6;
                    Debug.Log("Retomando movimento para a direita.");
                    isPaused = false; // Retoma o movimento
                    lightRight.intensity = lightRight.intensity <= 0 ? 6 : 0;
                }

                // Aplica a velocidade e anima��o com base na dire��o
                if (isMovingRight)
                {
                    motorTranslation.motorSpeed = -2;
                    anim.SetBool("isRight", true);
                    anim.SetBool("isLeft", false); // Garante que a anima��o esquerda esteja desativada
                    // Certifique-se de que "isDuck" seja controlado por uma l�gica espec�fica se n�o for constante
                    anim.SetBool("isDuck", false);
                }
                else
                {
                    motorTranslation.motorSpeed = 2;
                    anim.SetBool("isLeft", true);
                    anim.SetBool("isRight", false); // Garante que a anima��o direita esteja desativada
                    // Certifique-se de que "isDuck" seja controlado por uma l�gica espec�fica se n�o for constante
                    anim.SetBool("isDuck", false);
                }
                sliderPlayer.motor = motorTranslation;
            }
            yield return null; // Espera um frame antes de continuar o loop

            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enable_Play"))
        {
            Debug.Log("Colis�o com colet�vel detectada!");
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
                //interactionText.text = "Pressione E para interagir!";
            }
            // Opcional: Destr�i o objeto colidido
            // Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enable_Play"))
        {
            Debug.Log("Saiu da colis�o com colet�vel.");
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }
}
