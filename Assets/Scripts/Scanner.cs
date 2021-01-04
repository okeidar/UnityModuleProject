using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner
{
    private IScanable m_ScannedObject;
    private Transform m_ScanerTransform;
    private float m_ScanDistance = 2f;

    public Scanner(Transform scanerTransform)
    {
        m_ScanerTransform = scanerTransform;
        
    }

    public void Scan()
    {
        var hit = Physics2D.Raycast(m_ScanerTransform.position, m_ScanerTransform.up, m_ScanDistance);
        
        if (hit.collider == null)
        {
            Debug.Log("No Object Found");
            return;
        }
        var objecHit = hit.transform.gameObject;
        var scannableObject = objecHit.GetComponent<IScanable>();
        if (scannableObject != null)
        {
            Debug.Log("Scanable Object Found!");
            scannableObject.Scanning();
            m_ScannedObject = scannableObject;
        }
    }
    
    public IScanable Deploy()
    {
        Debug.Log("Deploy Obj");
        //instanciate Object from m_ScannedObject.Deploy()
        return null;
    }

    
}