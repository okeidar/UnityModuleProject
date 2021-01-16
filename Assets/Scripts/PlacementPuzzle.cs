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

        Scannable m_previewKeyItem;

        protected override void TriggerEntered(Collider2D collision)
        {
            var keyItem = collision.GetComponent<KeyItem>();
            if (TrySolve(keyItem))
            {
                var scannable = collision.GetComponent<Scannable>();
                if (scannable && scannable.IsPreview)
                {
                    m_previewKeyItem = scannable;
                    m_previewKeyItem.OnDeployed += KeyDeployed;
                }
                else
                {
                    OnSolve(keyItem.gameObject);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(m_previewKeyItem)
            {
                m_previewKeyItem.OnDeployed -= KeyDeployed;
                m_previewKeyItem = null;
            }
        }

        private void KeyDeployed(GameObject obj)
        {
            OnSolve(obj);
            m_previewKeyItem.OnDeployed -= KeyDeployed;
            m_previewKeyItem = null;
        }

        public override bool TrySolve(KeyItem item)
        {
            return item.KeyID == solveId;
        }
        protected override void OnSolve(GameObject solutionItem)
        {
            PuzzleSolved = true;
            solutionItem.transform.position = gameObject.transform.position; //snapping
            base.OnSolve(solutionItem);
        }
    }
}
