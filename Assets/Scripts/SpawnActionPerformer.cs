using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnActionPerformer : ActionPerformer
{
    [SerializeField] GameObject whatToSpawn;
    [SerializeField] Transform whereToSpawn;

    public override void PerformAction()
    {
        Instantiate(whatToSpawn, whereToSpawn.position,Quaternion.identity);
    }
}
