using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEnter4 : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject cutsceneCam;
    public GameObject cutscene;
    public GameObject dialog1;
    public GameObject dialog2;
    public GameObject dialog3;
    public GameObject dialog4;
    public GameObject anubis;
    public GameObject isis;
    public GameObject map4;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        thePlayer.SetActive(false);
        cutscene.SetActive(true);
        dialog1.SetActive(true);
        dialog2.SetActive(true);
        dialog3.SetActive(true);
        dialog4.SetActive(true);
        anubis.SetActive(true);
        isis.SetActive(true);
        map4.SetActive(true);
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(34);
        thePlayer.SetActive(true);
        cutscene.SetActive(false);
        cutsceneCam.SetActive(false);
        dialog1.SetActive(false);
        dialog2.SetActive(false);
        dialog3.SetActive(false);
        dialog4.SetActive(false);
        anubis.SetActive(false);
        isis.SetActive(false);
        map4.SetActive(false);
        SceneManager.LoadScene("Level4Scene");
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
