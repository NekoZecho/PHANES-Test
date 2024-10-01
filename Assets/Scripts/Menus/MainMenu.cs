using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string levelToLoad;

    public void quit()
    {
        Application.Quit();
    }

    public void gameStart()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
