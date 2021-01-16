using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : Puzzle
{
    [SerializeField] private Collider2D m_DoorCollider;


    public override bool TrySolve(Pickable item)
    {
        return base.TrySolve(item);
    }

    protected override void OnSolve()
    {
        m_anim.SetBool("Solved", true);
        m_DoorCollider.isTrigger = true;
    }
}
