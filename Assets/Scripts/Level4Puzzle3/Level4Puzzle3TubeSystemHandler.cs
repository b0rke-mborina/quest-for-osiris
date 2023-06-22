using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Puzzle3TubeSystemHandler : MonoBehaviour
{


    GameObject puzzle;

    public bool isInOriginalState;

    // Start is called before the first frame update
    void Start()
    {


        isInOriginalState = true;
    }

    // Update is called once per frame
    public void SetToOriginalState()
    {
        isInOriginalState = true;
    }
}
