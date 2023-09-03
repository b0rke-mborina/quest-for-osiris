using UnityEngine;

public class ActivateDoor : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;
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
            door1.GetComponent<Animator>().Play("GoLeft");
            door2.GetComponent<Animator>().Play("GoRight");
            wallAnimationPlayed = true;

            Invoke("StartChestAnimation", 3f);
        }
    }

    private void StartChestAnimation()
    {
        GameObject chest = GameObject.Find("Chest02");
        if (chest != null)
        {
            Animator chestAnimator = chest.GetComponent<Animator>();
            if (chestAnimator != null)
            {
                chestAnimator.Play("ChestOpen2");
            }
        }
    }

    public bool CheckCombination()
    {
        float boxDistance = CalculateDistance("boxActivate");

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
