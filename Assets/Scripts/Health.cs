using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public GameObject DeathCanvas;
    public GameObject MainCam;
    public int health;
    public int maxHealth= 10;
    private bool cursorWasLocked = true;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame


    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
            MainCam.SetActive(false);
            DeathCanvas.SetActive(true);
            StartCoroutine(Finish());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorWasLocked = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(5);
        MainCam.SetActive(true);
        DeathCanvas.SetActive(false);

    }
}
