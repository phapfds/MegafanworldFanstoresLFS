using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewSceneButton : MonoBehaviour
{
    public void LoadNewScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void LoadNewSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    public void LoadNewSceneInIAP(string SceneName)
    {
        SceneManager.UnloadSceneAsync("IAP");
        SceneManager.LoadScene(SceneName);
    }
    public void QuitGame()
    {
        GameObject quitGame = GameObject.Find("QuitGame1920_1080(Clone)");
        if (quitGame == null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            quitGame = Instantiate(Resources.Load("UI/QuitGame1920_1080"), canvas.transform) as GameObject;
        }
    }
}
