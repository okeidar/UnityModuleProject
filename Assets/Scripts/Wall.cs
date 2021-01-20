using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Collider2D m_Collider;
    private Scannable m_l;
    private void Awake() 
    {
        m_Collider = gameObject.GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other){
        var l = other.gameObject.GetComponent<Scannable>();
        if(l)
        {
            m_l = l;
            m_l.OnDeployed += M_l_OnDeployed;
        }
    }
    
    private void M_l_OnDeployed(GameObject obj)
    {
        m_Collider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        var l = other.gameObject.GetComponent<Scannable>();
        if(l && m_l == l)
        {
            m_l.OnDeployed -= M_l_OnDeployed;
            m_l = null;
            return;
        }
        var player = other.gameObject.GetComponent<PlayerController>();
        if(player && !m_l)
        {
            m_Collider.isTrigger = false;
        }
    }
}
