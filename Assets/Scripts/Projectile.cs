using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed=35;
    [SerializeField] int damage = 25;
    [SerializeField] float lifetimeSeconds = 3;

    private void Start()
    {
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
        Vector2 normalizedDeltaPos = speed * Time.deltaTime * Vector3.up;
        transform.Translate(normalizedDeltaPos);
            
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
