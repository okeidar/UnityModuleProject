using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{

    [SerializeField] int health=100;

    private int m_currentHealth;

    private void Start()
    {
        m_currentHealth = health;
    }

    public void Damage(int damage)
    {
        m_currentHealth -= damage;
        Debug.Log(m_currentHealth);
        if (m_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
