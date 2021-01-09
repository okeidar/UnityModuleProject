using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private ScannableObject m_ScannedObject;
    [SerializeField] Transform m_ScanerTransform;
    [SerializeField] float m_ScanDistance = 2f;

 

    public void Scan()
    {
        var hit = Physics2D.Raycast(m_ScanerTransform.position, m_ScanerTransform.up, m_ScanDistance, ~(LayerMask.GetMask("Player")));
       
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
            m_ScannedObject = scannableObject.Scanning();
        }
    }

    public IScanable Deploy()
    {
        Debug.Log("Deploy Obj");
        Instantiate(m_ScannedObject.grantedObjectPrefab, transform.position + transform.up * m_ScanDistance,Quaternion.identity);
        //instanciate Object from m_ScannedObject.Deploy()
        return null;
    }


}