using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 35;
    [SerializeField] int damage = 25;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var healthComponent = collision.gameObject.GetComponent<HealthObject>();
        if (healthComponent)
        {
            healthComponent.Damage(damage);
        }
        Destroy(gameObject);
    }
}
