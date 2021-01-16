using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlacementPuzzle : Puzzle
    {
        [SerializeField] int solveId;

        protected override void TriggerEntered(Collider2D collision)
        {
            var keyItem = collision.GetComponent<KeyItem>();
            if(TrySolve(keyItem))
            {
                OnSolve(keyItem.gameObject);
            }
        }
        public override bool TrySolve(KeyItem item)
        {
            return item.KeyID == solveId;
        }
        protected override void OnSolve(GameObject solutionItem)
        {
            PuzzleSolved = true;
            solutionItem.transform.position = gameObject.transform.position; //snapping
        }
    }
}
