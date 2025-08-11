using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Text.RegularExpressions;

public class Thunder_Controller : MonoBehaviour
{
    // --- Configuracoes de audio ---
    [SerializeField] private AudioClip thunderSoundClip; 
    [SerializeField] private AudioSource thunderSource;
    [SerializeField] private AudioSource musicSource;

    // --- Configuracoes de Luzes ---
    private string lightTag = "SpotLight_Menu"; // Tag das Spotlights
    private float flashIntensity = 1.5f; // Intensidade das luzes
    private float flashDuration = 0.50f; // Duracao da transicao de cada luz
    private float delayBetweenLights = 0.25f; // Atrasa a mudanca de cada luz

    // --- Configuracoes das Sprites ---
    private string spriteTag = "RaySprite_Menu";

    // --- Configuracoes da Sequência ---
    private float initialDelay = 3f;
    private float sequenceCooldown = 10f;

    // --- Referencias de Objetos Internos ---
    private Light2D[] thunderLights;
    private SpriteRenderer[] raySprites;
    private float nextSequenceTime;
    private bool sequenceRunning = false;
    private float musicVolumeOld;
    // Start is called before the first frame update
    void Start()
    {
        //thunderSource = GetComponent<AudioSource>();
        //musicSource = GetComponent<AudioSource>();
        if (thunderSource == null && musicSource == null)
        {
            Debug.LogWarning("AudioSource component not found on this GameObject. Thunder sound will not play.");
        }

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

            thunderLights = validLights.OrderBy(l => int.Parse(Regex.Match(l.name, @"\d+").Value)).ToArray(); 
        }
        else
        {
            Debug.LogWarning($"Nenhum objeto com a tag '{lightTag}' encontrado.");
        }

        GameObject[] spriteObjects = GameObject.FindGameObjectsWithTag(spriteTag);
        if (spriteObjects.Length > 0)
        {
            List<SpriteRenderer> validSprites = new List<SpriteRenderer>();
            foreach (GameObject obj in spriteObjects)
            {
                SpriteRenderer currentSprite = obj.GetComponent<SpriteRenderer>();
                if (currentSprite != null)
                {
                    validSprites.Add(currentSprite);
                    currentSprite.enabled = false;
                }
                else
                {
                    Debug.LogWarning($"Objeto com tag '{spriteTag}' não tem um componente SpriteRenderer e foi ignorado: {obj.name}");
                }
            }

            raySprites = validSprites.OrderBy(s => int.Parse(Regex.Match(s.name, @"\d+").Value)).ToArray();

            if (thunderLights.Length != raySprites.Length)
            {
                Debug.LogWarning("Número de luzes e sprites com as tags especificadas não corresponde! " +
                                 "Isso pode causar problemas na sincronização. Luzes: " + thunderLights.Length +
                                 ", Sprites: " + raySprites.Length);
            }
        }
        else
        {
            Debug.LogWarning($"Nenhum objeto com a tag '{spriteTag}' encontrado.");
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
            SpriteRenderer currentSprite = (i < raySprites.Length) ? raySprites[i] : null;

            if (currentLight != null)
            {
                StartCoroutine(FlashLightSprite(currentLight, currentSprite, flashIntensity, flashDuration));
                if (i > 0)
                {
                    Light2D previousLight = thunderLights[i - 1];
                    SpriteRenderer previousSprite = (i - 1 < raySprites.Length) ? raySprites[i - 1] : null;
                    StartCoroutine(TurnOff(previousLight, previousSprite, delayBetweenLights));
                }

                yield return new WaitForSeconds(delayBetweenLights);
            }
        }

        if (thunderLights.Length > 0 && thunderLights[thunderLights.Length - 1] != null)
        {
            yield return new WaitForSeconds(delayBetweenLights);
            thunderLights[thunderLights.Length - 1].intensity = 0f;
            if (raySprites.Length > 0 && raySprites[thunderLights.Length - 1] != null)
            {
                raySprites[thunderLights.Length - 1].enabled = false;
            }
        }

        // --- NOVO: Atraso para o som do trovão e toca o som ---
        yield return new WaitForSeconds(2f); // Espera 3 segundos após a última luz ser apagada

        musicVolumeOld = musicSource.volume;
        if (thunderSource != null && thunderSoundClip != null)
        {
            musicSource.volume = musicVolumeOld * 0.4f;
            thunderSource.PlayOneShot(thunderSoundClip); // Toca o som do trovão
            yield return new WaitForSeconds(2f);
            musicSource.volume = musicVolumeOld;
            //Debug.Log("Som de trovão tocado!");
        }
        else
        {
            if (thunderSource == null) Debug.LogWarning("AudioSource não encontrado para tocar o som do trovão.");
            if (thunderSoundClip == null) Debug.LogWarning("AudioClip do trovão não atribuído no Inspetor.");
        }

        nextSequenceTime = Time.time + sequenceCooldown;
        sequenceRunning = false;
    }

    IEnumerator FlashLightSprite(Light2D targetLight2D, SpriteRenderer targetSprite, float targetIntensity, float duration)
    {
        if (targetSprite != null)
        {
            targetSprite.enabled = true;
        }

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            targetLight2D.intensity = Mathf.Lerp(0f, targetIntensity, timer / duration);
            yield return null;
        }
        targetLight2D.intensity = targetIntensity;
    }

    IEnumerator TurnOff(Light2D lightToTurnOff, SpriteRenderer spriteToHide, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (lightToTurnOff != null)
        {
            lightToTurnOff.intensity = 0f;
        }
        if (spriteToHide != null)
        {
            spriteToHide.enabled = false; // Esconde o sprite
        }
    }

}
