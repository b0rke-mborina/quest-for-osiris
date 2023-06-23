using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth;
    [SerializeField]public float CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth= MaxHealth;
        
    }

    // Update is called once per frame
    public void TakeDamage(float Amount) {
        CurrentHealth -= Amount;

        if(CurrentHealth <= 0) {
            Debug.Log("You Died");
            CurrentHealth= MaxHealth;
        }
    }
}
