using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPerformer : MonoBehaviour
{
    public virtual void PerformAction()
    {
        Debug.LogWarning("No action assigned");
    }
}
