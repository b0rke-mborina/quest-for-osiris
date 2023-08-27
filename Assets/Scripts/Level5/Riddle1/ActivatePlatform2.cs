using UnityEngine;

public class ActivatePlatform2 : MonoBehaviour
{
    private Renderer spotRenderer;
    public delegate void ColorChangeEventHandler();
    public event ColorChangeEventHandler OnColorChanged;

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

        // Notify color change
        if (isCorrectCombination)
        {
            OnColorChanged?.Invoke();
        }
    }

    public bool CheckCombination()
    {
        float boxDistance = CalculateDistance("CoinGold2");
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

    // Add a property to check if the platform is green
    public bool IsGreen
    {
        get { return spotRenderer.material.color == Color.green; }
    }
}
