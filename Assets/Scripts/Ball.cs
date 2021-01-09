using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IScanable
{
    [SerializeField] ScannableObject grantedObect;

    public void Deploy()
    {
        Debug.Log("Ball Deployed");
    }
    public ScannableObject Scanning()
    {
        Debug.Log("Ball Scanned");
        return grantedObect;
    }
}
