using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mummy_AI : MonoBehaviour
{
    NavMeshAgent _agent;
    Animator _animator;

    public GameObject _Target;

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
        _agent.SetDestination(_Target.transform.position);

        //Animation
        if (_agent.isStopped)
        {
            _animator.SetBool("walk", false);
        }
        else
        {
            _animator.SetBool("walk", true);
        }
        
    }
}
