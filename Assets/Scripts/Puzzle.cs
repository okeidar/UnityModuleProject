using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] protected KeyObject keyToSolve;
    [SerializeField] protected Animator m_Anim;

    private bool PuzzleSolved;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            TrySolve(player.GetObjectInHand());
        }
    }
    public virtual bool TrySolve(Pickable item)
    {
        Debug.Log($"compare: {item.GetType()}");
        PuzzleSolved = item is KeyObject && keyToSolve.KeyId == ((KeyObject)item).KeyId;  
        if(PuzzleSolved)
        {
            OnSolve();
        }
        return PuzzleSolved;
    }
    protected virtual void OnSolve()
    {
        Destroy(gameObject);
    }
}
