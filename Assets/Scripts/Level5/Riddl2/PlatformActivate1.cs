using UnityEngine;

public class PlatformActivate1 : MonoBehaviour
{
    public GameObject wall;
    private Renderer spotRenderer;

    private bool wallAnimationPlayed = false;

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
            PlayWallAnimation();
        }
    }

    private void PlayWallAnimation()
    {
        if (!wallAnimationPlayed)
        {
            wall.GetComponent<Animator>().Play("WallUp");
            wallAnimationPlayed = true;
        }
    }

    public bool CheckCombination()
    {
        float boxDistance = CalculateDistance("box1");

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
