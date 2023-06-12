using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float MaxSpeed;
    private float Speed;

    private Collider[] hitColliders;
    private RaycastHit Hit;
    public float SightRange;
    public float DetectionRange;
    public Rigidbody rb;
    public GameObject Target;
    public float Damage;
    public float KOTime;
    private bool CanAttack= true;

    private bool seePlayer;
    // Start is called before the first frame update
    void Start()
    {
        Speed= MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!seePlayer) {
            hitColliders= Physics.OverlapSphere(transform.position, DetectionRange);
            foreach (var HitCollider in hitColliders)
            {
                if(HitCollider.tag=="Player") {
                    Target= HitCollider.gameObject;
                    seePlayer= true;
                }
            }
        }
        else {
            if(Physics.Raycast(transform.position, (Target.transform.position - transform.position), out Hit, SightRange)) {
                if(Hit.collider.tag== "Player") {
                    seePlayer= false;
                }
                else {
                    var Heading= Hit.transform.position - transform.position;
                    var Distance= Heading.magnitude;
                    var Direction= Heading / Distance;


                    Vector3 Move= new Vector3(Direction.x * Speed, 0, Direction.z * Speed);
                    rb.velocity= Move;
                    transform.forward= Move;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag== "Player") {
        collision.collider.gameObject.GetComponent<Health>().TakeDamage(Damage);
        StartCoroutine(AttackDelay(KOTime));
        }
    }

    IEnumerator AttackDelay(float Delay) {
        Speed= 0;
        CanAttack= false;
        yield return new WaitForSeconds(Delay);
        Speed= MaxSpeed;
        CanAttack= true;
    }
}
