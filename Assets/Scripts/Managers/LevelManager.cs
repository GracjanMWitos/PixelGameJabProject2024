using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    
    public void SelectScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void SelectSceneByIndex(int index)
    {

        SceneManager.LoadScene(index);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
