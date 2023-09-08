using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool GameIsPaused = false;
    PlayerCameraController camController;

    private bool cursorWasLocked = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorWasLocked = false;
        camController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCameraController>();
        camController.isPaused = true;

    }

    public void Resume()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        camController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCameraController>();
        camController.isPaused = false;
        if (!cursorWasLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main menu");
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorWasLocked = true;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorWasLocked = false;
    }
}
