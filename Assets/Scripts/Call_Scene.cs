using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Call_Scene : MonoBehaviour
{
    public void CallSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CallSceneWithIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
