using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Thunder_Controller : MonoBehaviour
{
    public string lightTag = "SpotLight_Menu"; // Tag das Spotlights
    public float flashIntensity = 1.5f; // Intensidade das luzes
    public float flashDuration = 0.25f; // Duração da transição de cada luz
    public float delayBetweenLights = 0.125f; // Atrasa a mudança de cada luz
    public float initialDelay = 3f;
    public float sequenceCooldown = 10f;

    private Light2D[] thunderLights;
    private float nextSequenceTime;
    private bool sequenceRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        // Encontra todos os objetos com a tag especificada
        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag(lightTag);
        if (lightObjects.Length > 0)
        {
            List<Light2D> validLights = new List<Light2D>();

            foreach (GameObject obj in lightObjects)
            {
                Light2D currentLight = obj.GetComponent<Light2D>();
                if (currentLight != null)
                {
                    validLights.Add(currentLight);
                    currentLight.intensity = 0f;
                }
                else
                {
                    Debug.LogWarning($"Objeto com tag '{lightTag}' não é uma Spotlight e foi ignorado: {obj.name}");
                }
            }

            thunderLights = validLights.OrderBy(l => int.Parse(System.Text.RegularExpressions.Regex.Match(l.name, @"\d+").Value)).ToArray(); // Converte a lista para array
        }
        else
        {
            Debug.LogWarning($"Nenhum objeto com a tag '{lightTag}' encontrado.");
        }

        nextSequenceTime = Time.time + initialDelay;
    }

    void Update()
    {
        if (!sequenceRunning && Time.time >= nextSequenceTime)
        {
            StartCoroutine(ThunderSequence());
        }
    }

    IEnumerator ThunderSequence()
    {
        sequenceRunning = true;

        for (int i = 0; i < thunderLights.Length; i++)
        {
            Light2D currentLight = thunderLights[i];
            if (currentLight != null)
            {
                StartCoroutine(FlashLight(currentLight, flashIntensity, flashDuration));
                if (i > 0)
                {
                    StartCoroutine(TurnOffLight(thunderLights[i - 1], delayBetweenLights));
                }

                yield return new WaitForSeconds(delayBetweenLights);
            }
        }

        if (thunderLights.Length > 0 && thunderLights[thunderLights.Length - 1] != null)
        {
            yield return new WaitForSeconds(delayBetweenLights);
            thunderLights[thunderLights.Length - 1].intensity = 0f;
        }

        nextSequenceTime = Time.time + sequenceCooldown;
        sequenceRunning = false;
    }

    IEnumerator FlashLight(Light2D targetLight2D, float targetIntensity, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            targetLight2D.intensity = Mathf.Lerp(0f, targetIntensity, timer / duration);
            yield return null;
        }
        targetLight2D.intensity = targetIntensity;
    }

    IEnumerator TurnOffLight(Light2D lightToTurnOff, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (lightToTurnOff != null)
        {
            lightToTurnOff.intensity = 0f;
        }
    }

}
