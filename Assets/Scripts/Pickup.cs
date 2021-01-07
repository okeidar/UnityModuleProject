using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Weapon grantedItem; //TODO: should be more general
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.GrantWeapon(grantedItem);
        }
        Destroy(gameObject);
    }
}
