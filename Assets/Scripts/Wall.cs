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
            m_Collider.isTrigger = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        var l = other.gameObject.GetComponent<Scannable>();
        if(l && m_l == l)
        {
            m_Collider.isTrigger = true;
        }
    }
}
