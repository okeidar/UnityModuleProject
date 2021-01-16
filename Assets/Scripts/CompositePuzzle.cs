using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CompositePuzzle : Puzzle
    {
        List<Puzzle> puzzles;
        ActionPerformer actionOnSolve;

        private void Start()
        {
            actionOnSolve = GetComponent<ActionPerformer>();
            puzzles = GetComponentsInChildren<Puzzle>().ToList();
            puzzles.RemoveAt(0);// removing self

            puzzles.ForEach(p => p.OnPuzzleSolved += CompositePuzzleSolved);
        }

        private void CompositePuzzleSolved(UnityEngine.GameObject obj)
        {
            bool allSolved = puzzles.All(p=>p.PuzzleSolved);
            if (allSolved)
            {
                OnSolve();
            }
        }

        protected override void OnSolve()
        {
            if (actionOnSolve)
            {
                actionOnSolve.PerformAction();
            }
        }


    }
}
