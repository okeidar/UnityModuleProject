using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 35;
    [SerializeField] protected int damage = 25;
    [SerializeField] float lifetimeSeconds = 3;



    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
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
                Destroy(gameObject);
                return;
            }
            healthComponent.Damage(damage);
        }
        Destroy(gameObject);
        // TODO do we want the perview object to block the projectile?
        /*
        var scannable = collision.gameObject?.GetComponent<Scannable>();
        if(!scannable || !scannable.IsPreview)
        {
            Destroy(gameObject);
        }
        */

    }
}
