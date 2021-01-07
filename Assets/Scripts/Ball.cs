using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IScanable
{

    public void Deploy()
    {
        Debug.Log("Ball Deployed");
    }
    public void Scanning()
    {
        Debug.Log("Ball Scanned");
    }
}
