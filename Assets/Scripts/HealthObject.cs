using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{

    [SerializeField] public int health=100;

    private int m_currentHealth;
    private PlayerController m_player;

    private void Start()
    {
        m_currentHealth = health;
        m_player = gameObject.GetComponent<PlayerController>();
    }

    public void Damage(int damage)
    {
        m_currentHealth -= damage;
        if (m_currentHealth <= 0)
        {
            if(m_player)
            {
                m_player?.Kill();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        m_player?.UpdateLife(m_currentHealth);
    }
}
