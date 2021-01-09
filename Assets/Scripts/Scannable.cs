using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scannable : MonoBehaviour
{
    [SerializeField] ScannableObject grantedObect;

    public void Deploy()
    {
        Debug.Log("ScannableObject Deployed");
    }
    public ScannableObject Scanning()
    {
        Debug.Log("ScannableObject Scanned");
        return grantedObect;
    }
}
