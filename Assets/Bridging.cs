using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridging : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.tag = tag;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.tag = "Untagged";
        Debug.Log("Tag is now " + collision.gameObject.tag);
    }
}
