using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadScene : MonoBehaviour
{
    public GameObject textGameObject;
    private bool isPlayerInWater;
    private float timer;

    void Update()
    {
        if (isPlayerInWater)
        {
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                ReloadCurrentScene();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (!isPlayerInWater)
            {
                isPlayerInWater = true;
                textGameObject.SetActive(true);
                StartCoroutine(HideTextAfterDelay(2f));
            }
        }
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textGameObject.SetActive(false);
    }

    void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
