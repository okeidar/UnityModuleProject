using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] bool instaKill = true;
    [SerializeField] string PreventionTag ;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hazard!");
        if(collision.gameObject.CompareTag(PreventionTag))
        {
            Debug.Log("Saved!");
            return;
        }
        if (instaKill)
        {
            var healthComponent = collision.gameObject.GetComponent<HealthObject>();
            if (healthComponent)
            {
                healthComponent.Damage(healthComponent.CurrentHealth);
            }
        }
    }
}
