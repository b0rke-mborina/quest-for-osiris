using UnityEngine;

public class KinematicControl : MonoBehaviour
{
    public GameObject box3Object;
    public GameObject desertTempleObject;

    private Rigidbody box3Rigidbody;
    private Animator columnAnimator;
    private bool animationPlayed;
    private float animationStartTime;

    private void Start()
    {
        box3Rigidbody = box3Object.GetComponent<Rigidbody>();
        columnAnimator = desertTempleObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (columnAnimator.GetCurrentAnimatorStateInfo(0).IsName("ColumnDown"))
        {
            if (!animationPlayed)
            {
                animationPlayed = true;
                animationStartTime = Time.time;
            }

            if (Time.time - animationStartTime >= 2.0f)
            {
                box3Rigidbody.isKinematic = true;
            }
            else
            {
                box3Rigidbody.isKinematic = false;
            }
        }
        else
        {
            animationPlayed = false;
            box3Rigidbody.isKinematic = true;
        }
    }
}
