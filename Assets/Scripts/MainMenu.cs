using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene("Level menu");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controls menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
