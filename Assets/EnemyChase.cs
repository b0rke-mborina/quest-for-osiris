using UnityEngine;
using UnityEngine.AI;

    public class EnemyChase : MonoBehaviour
    {
        public Transform player;  // Reference to the player's transform.
        public float chaseSpeed = 5.0f;  // Speed at which the enemy chases the player.
        public float stoppingDistance = 30.0f;  // Distance at which the enemy stops chasing.

        private void Update()
        {
            // Calculate the distance between the enemy and the player.
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if the player is within the stopping distance.
            if (distanceToPlayer < stoppingDistance)
            {
                // Move the enemy towards the player.
                transform.LookAt(player);  // Rotate the enemy to face the player.
                transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);
            }
            // You can add additional behavior if the enemy is within stopping distance here.
        }
    }