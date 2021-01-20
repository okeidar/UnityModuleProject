using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentAI : MonoBehaviour
{
    [SerializeField] Collider2D rangeTrigger;
    [SerializeField] LayerMask targetLayerMask;

    Shooter m_shooter;
    bool m_isInRange = false;
    GameObject m_target = null;

    // Start is called before the first frame update
    void Start()
    {
        m_shooter = GetComponent<Shooter>();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_isInRange && m_target != null)
        {
            m_shooter.ShootAt(m_target.transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (((1 << collision.gameObject.layer) & targetLayerMask) != 0)
        {
            m_isInRange = true;
            m_target = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_target != null && collision.gameObject == m_target)
        {
            m_isInRange = false;
            m_target = null;
        }
    }

}
