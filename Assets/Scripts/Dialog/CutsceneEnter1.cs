using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnter1 : MonoBehaviour
{

    public GameObject thePlayer;
    public GameObject cutsceneCam;
    public GameObject cutscene;
    public GameObject dialog1;
    public GameObject map1;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        thePlayer.SetActive(false);
        cutscene.SetActive(true);
        dialog1 .SetActive(true);
        map1.SetActive(true);
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(43);
        thePlayer.SetActive(true);
        cutscene.SetActive(false);
        map1 .SetActive(false);
        cutsceneCam.SetActive(false);
        dialog1.SetActive(false);
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
