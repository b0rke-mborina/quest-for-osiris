using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEnter6 : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject cutsceneCam;
    public GameObject cutscene;
    public GameObject dialog1;
    public GameObject dialog2;
    public GameObject dialog3;
    public GameObject dialog4;
    public GameObject dialog5;
    public GameObject dialog6;
    public GameObject isis;
    public GameObject anubis;

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
        dialog5.SetActive(true);
        dialog6.SetActive(true);
        isis.SetActive(true);
        anubis.SetActive(true);
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(43);
        thePlayer.SetActive(true);
        cutscene.SetActive(false);
        cutsceneCam.SetActive(false);
        dialog1.SetActive(false);
        dialog2.SetActive(false);
        dialog3.SetActive(false);
        dialog4.SetActive(false);
        dialog5.SetActive(false);
        dialog6.SetActive(false);
        anubis.SetActive(false);
        isis.SetActive(false);
        SceneManager.LoadScene("Level Menu");
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