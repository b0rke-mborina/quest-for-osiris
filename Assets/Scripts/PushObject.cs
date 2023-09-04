using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            Quaternion originalRotation = rigidbody.rotation;
            rigidbody.freezeRotation = true;

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);

            // Check the spot combination after pushing a cube
            GameObject spot = GameObject.Find("Spot");
            if (spot != null)
            {
                PlaceCubeObject placeCubeObject = spot.GetComponent<PlaceCubeObject>();
                placeCubeObject.CheckCombination();
            }
        }
    }
}
