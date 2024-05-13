using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
  public void StartGame()
    {
        SceneManager.LoadScene("EnemiesTest");
    }

    
    public void SelectScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

   
    public void QuitGame()
    {
        Application.Quit();
    }
}
