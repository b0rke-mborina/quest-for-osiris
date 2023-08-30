using UnityEngine;

public class Playerfollow : MonoBehaviour
{
    public Transform Playerpos;
    UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
       agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update()
    {
        
          agent.destination = Playerpos.position;    
        
    }
}