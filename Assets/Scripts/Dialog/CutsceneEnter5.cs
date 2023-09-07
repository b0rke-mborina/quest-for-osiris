using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEnter5 : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject cutsceneCam;
    public GameObject cutscene;
    public GameObject dialog1;
    public GameObject dialog2;
    public GameObject dialog3;
    public GameObject isis;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        thePlayer.SetActive(false);
        cutscene.SetActive(true);
        dialog1.SetActive(true);
        dialog2.SetActive(true);
        dialog3.SetActive(true);
        isis.SetActive(true);
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(24);
        thePlayer.SetActive(true);
        cutscene.SetActive(false);
        cutsceneCam.SetActive(false);
        dialog1.SetActive(false);
        dialog2.SetActive(false);
        dialog3.SetActive(false);
        isis.SetActive(false);
        SceneManager.LoadScene("Level5Scene");
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