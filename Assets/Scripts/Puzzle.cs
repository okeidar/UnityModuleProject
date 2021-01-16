using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] protected KeyObject keyToSolve;
    [SerializeField] protected Animator m_anim;
    [SerializeField] protected string m_animSolveBooleanName;    
    
    public bool PuzzleSolved { get; protected set; }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEntered(collision);
    }

    protected virtual void TriggerEntered(Collider2D collision)
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
    public virtual bool TrySolve(KeyItem item)
    {
        return false;
    }
    protected virtual void OnSolve()
    {
        Destroy(gameObject);
    }
    protected virtual void OnSolve(GameObject solutionItem)
    {
    }
}
