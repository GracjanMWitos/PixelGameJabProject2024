using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class LevelPaths
{
    public string easyPath;
    public string hardPath;
}

public class LevelManager : MonoBehaviour
{
    public List<LevelPaths> levelPaths = new List<LevelPaths>();
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
