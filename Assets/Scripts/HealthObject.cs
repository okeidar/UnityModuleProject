using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{

    [SerializeField] int health=100;
    private PlayerController m_player;


    public int CurrentHealth { get ; private set; }

    private void Start()
    {
        CurrentHealth = health;
        m_player = gameObject.GetComponent<PlayerController>();
    }

    public void Damage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(CurrentHealth);
        if (CurrentHealth <= 0)
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
        m_player?.UpdateLife(CurrentHealth);
    }
}
