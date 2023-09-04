using UnityEngine;

public class PlatformActivate3 : MonoBehaviour
{
    public GameObject platform;
    private Renderer spotRenderer;

    private bool platformAnimationPlayed = false;

    private void Start()
    {
        spotRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        SetSpotColor();
    }

    private void SetSpotColor()
    {
        bool isCorrectCombination = CheckCombination();
        spotRenderer.material.color = isCorrectCombination ? Color.green : Color.red;

        if (isCorrectCombination)
        {
            PlayPlatformAnimation();
        }
    }

    private void PlayPlatformAnimation()
    {
        if (!platformAnimationPlayed)
        {
            platform.GetComponent<Animator>().Play("PlatformDown");
            platformAnimationPlayed = true;
        }
    }

    public bool CheckCombination()
    {
        float boxDistance = CalculateDistance("box3");

        return boxDistance < 0.6f;
    }


    private float CalculateDistance(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj == null) return float.MaxValue;

        Collider objCollider = obj.GetComponent<Collider>();
        Collider spotCollider = GetComponent<Collider>();

        if (objCollider == null || spotCollider == null) return float.MaxValue;

        Vector3 objCenter = objCollider.bounds.center;
        Vector3 spotCenter = spotCollider.bounds.center;

        float distance = Vector3.Distance(objCenter, spotCenter);

        return distance;
    }
}
