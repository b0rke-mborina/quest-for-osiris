using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Level1Select()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Level3Select()
    {
        SceneManager.LoadScene("Level3Scene");
    }
    public void Level4Select()
    {
        SceneManager.LoadScene("Level4Scene");
    }
    public void Back()
    {
        SceneManager.LoadScene("Main menu");
    }
}

