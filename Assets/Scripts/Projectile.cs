using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 35;
    [SerializeField] protected int damage = 25;
    [SerializeField] float lifetimeSeconds = 3;

    Animator m_animator;
    Rigidbody2D rb;
    Collider2D m_collider;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        if(!m_animator)
        {
            m_animator = GetComponentInChildren<Animator>();
        }
        m_collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.LogError("Projectile must have a rigid body!");
            Destroy(gameObject);
        }

        rb.velocity = transform.up * speed;

        StartCoroutine(LifetimeTimer());
    }

    IEnumerator LifetimeTimer()
    {
        yield return new WaitForSeconds(lifetimeSeconds);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {


    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) //this is a very bad place to do that, it should be in a damageable interface or in the health script. 
    {
        var healthComponent = collision.gameObject.GetComponent<HealthObject>();
        if (healthComponent)
        {
            var vaulnerability = healthComponent as HealthWithVulnerability;
            if (vaulnerability != null && !gameObject.name.StartsWith(vaulnerability._weaknessBulletPrefab.name))
            {
                Die();
                return;
            }
            healthComponent.Damage(damage);
        }
        Die();

    }
    protected virtual void Die()
    {
        Destroy(rb);
        Destroy(m_collider);
        float delay = 0f;
        if(m_animator && m_animator.parameters.Any(m=>m.name=="Destroy"))
        {
            m_animator.SetBool("Destroy", true);
            delay = m_animator.GetCurrentAnimatorStateInfo(0).length;
        }
        Destroy(gameObject, delay);
    }
}
