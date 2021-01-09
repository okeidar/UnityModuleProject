using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Pickable grantedItem; //TODO: should be more general
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.TakeItem(grantedItem);
        }
        if(grantedItem.shouldDestroyOnPickup)
        {
            Destroy(gameObject);
        }
    }
}
