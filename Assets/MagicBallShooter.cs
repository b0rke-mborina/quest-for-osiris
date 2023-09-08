using UnityEngine;

public class MagicBallShooter : MonoBehaviour
{
    public GameObject magicBallPrefab; // The magic ball prefab (a combination of capsule and sphere).
    public Transform magicBallSpawnPoint; // The point where the magic ball will be instantiated.
    public float shootingForce = 10f; // The force at which the magic ball is shot.

    void Update()
    {
        // Check for player input to shoot (e.g., left mouse button).
        if (Input.GetButtonDown("Fire1"))
        {
            ShootMagicBall();
        }
    }

    void ShootMagicBall()
    {
        // Get the camera's forward vector instead of transform.forward.
        Vector3 shootingDirection = Camera.main.transform.forward;

        // Create the magic ball by instantiating the prefab.
        GameObject magicBall = Instantiate(magicBallPrefab, magicBallSpawnPoint.position, Quaternion.identity);

        // Get the rigidbody of the magic ball.
        Rigidbody magicBallRigidbody = magicBall.GetComponent<Rigidbody>();

        // Check if the magic ball has a rigidbody.
        if (magicBallRigidbody != null)
        {
            // Apply a force to the magic ball to shoot it in the camera's direction.
            magicBallRigidbody.AddForce(shootingDirection * shootingForce, ForceMode.Impulse);
        }
    }
}