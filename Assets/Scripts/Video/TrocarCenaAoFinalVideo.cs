using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TrocarCenaAoFinalVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nomeCena;

    void Start()
    {
        videoPlayer.loopPointReached += TrocarCena;
    }

    void TrocarCena(VideoPlayer vp)
    {
        SceneManager.LoadScene(nomeCena);
    }
}
