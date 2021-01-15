using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{

    [SerializeField] int health=100;


    public int CurrentHealth { get ; private set; }

    private void Start()
    {
        CurrentHealth = health;
    }

    public void Damage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
