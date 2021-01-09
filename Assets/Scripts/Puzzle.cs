using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private KeyObject keyToSolve;
    private bool PuzzleSolved;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            TrySolve(player.GetObjectInHand());
        }
    }
    public bool TrySolve(Pickable item)
    {
        Debug.Log($"compare: {item.GetType()}");
        PuzzleSolved = item is KeyObject && keyToSolve.KeyId == ((KeyObject)item).KeyId;  
        if(PuzzleSolved)
        {
            OnSolve();
        }
        return PuzzleSolved;
    }
    private void OnSolve()
    {
        Destroy(gameObject);
    }
}
