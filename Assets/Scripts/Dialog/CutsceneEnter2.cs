using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEnter2 : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject cutsceneCam;
    public GameObject cutscene;
    public GameObject cutsceneCharacter1;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        thePlayer.SetActive(false);
        cutscene.SetActive(true);
        cutsceneCharacter1.SetActive(true);
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(15);
        thePlayer.SetActive(true);
        cutscene.SetActive(false);
        cutsceneCharacter1.SetActive(false);
        cutsceneCam.SetActive(false);
        SceneManager.LoadScene("Level2Scene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}