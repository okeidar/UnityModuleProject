using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SplashProjectile : Projectile
{

    [SerializeField] float _radius = 1f;
    [SerializeField] LayerMask _hitLayer;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, _radius, _hitLayer);

        foreach (var hit in hits)
        {
            var healthComponent = hit.gameObject.GetComponent<HealthObject>();
            if (healthComponent)
            {
                healthComponent.Damage(damage);
            }
        }

        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.forward, _radius);
    }

}
