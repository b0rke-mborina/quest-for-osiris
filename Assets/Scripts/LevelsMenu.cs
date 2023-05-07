using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void LevelSelect()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Back()
    {
        SceneManager.LoadScene("Main menu");
    }
}

