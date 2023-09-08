using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MummyAI : MonoBehaviour
{
    NavMeshAgent _agent;
    Animator _animator;

    public GameObject _Target;
    public float chaseDistance = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _Target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _Target.transform.position);

        if (distanceToPlayer <= chaseDistance)
        {
            _agent.SetDestination(_Target.transform.position);

            // Animation
            if (_agent.velocity.sqrMagnitude <= 0.1f)
            {
                _animator.SetBool("walk", false);
            }
            else
            {
                _animator.SetBool("walk", true);
            }
        }
        else
        {
            // Stop chasing if player is out of range
            _agent.ResetPath();
            _animator.SetBool("walk", false);
        }
    }

    // Handle mouse click to destroy the enemy
    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}