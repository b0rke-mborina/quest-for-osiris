using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public GameObject wall;
    private Renderer spotRenderer;

    private bool wallAnimationPlayed = false;

    private void Start()
    {
        spotRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightSaber"))
        {
            SetSpotColor(Color.green);
            PlayWallAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightSaber"))
        {
            SetSpotColor(Color.red);
        }
    }

    private void SetSpotColor(Color color)
    {
        spotRenderer.material.color = color;
    }

    public void PlayWallAnimation()
    {
        if (!wallAnimationPlayed)
        {
            wall.GetComponent<Animator>().Play("DoorOpen");
            wallAnimationPlayed = true;
        }
    }
}