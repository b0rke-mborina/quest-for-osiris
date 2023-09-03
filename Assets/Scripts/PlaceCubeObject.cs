using UnityEngine;

public class PlaceCubeObject : MonoBehaviour
{
    public GameObject platform;
    public GameObject wall;
    private Renderer spotRenderer;

    [SerializeField] private Animator wallAnimator = null;

    private void Start()
    {
        spotRenderer = GetComponent<Renderer>();
        wallAnimator = wall.GetComponent<Animator>();
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
            wallAnimator.Play("Wall");
        }
    }

    public bool CheckCombination()
    {
        float cube1Distance = CalculateDistance("Cube1");
        float cube3Distance = CalculateDistance("Cube3");
        float cube5Distance = CalculateDistance("Cube5");

        return cube1Distance < 0.5f && cube3Distance < 0.1f && cube5Distance < 0.35f;
    }

    private float CalculateDistance(string cubeName)
    {
        GameObject cube = GameObject.Find(cubeName);
        if (cube == null) return float.MaxValue;

        Vector3 cubePosition = cube.transform.position;
        Vector3 spotPosition = transform.position;

        cubePosition = platform.transform.InverseTransformPoint(cubePosition);
        spotPosition = platform.transform.InverseTransformPoint(spotPosition);

        return Vector3.Distance(cubePosition, spotPosition);
    }
}
