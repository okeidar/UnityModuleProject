using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private ScannableObject m_ScannedObject;
    private Scannable m_ObectToScanInRange;

    [SerializeField] float m_DeployDistance = 2f;

 
    private void OnTriggerEnter2D(Collider2D other) 
    {
        var scannable = other.GetComponent<Scannable>();
        if(scannable)
        {
            m_ObectToScanInRange = scannable;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        var scannable = other.GetComponent<Scannable>();
        if(scannable == m_ObectToScanInRange)
        {
            m_ObectToScanInRange = null;
        }
    }
    public void Scan()
    {
      if (m_ObectToScanInRange)
        {
            Debug.Log("Scanable Object Found!");
            m_ScannedObject = m_ObectToScanInRange.Scanning();
            // TODO if Pickable set pickable.shouldDestroyOnPickup = true;
            /*
            var pickable = m_ScannedObject.grantedObjectPrefab.GetComponent<Pickable>();
            if(pickable)
            {
                pickable.shouldDestroyOnPickup = true;
            }
            */
        }
    }

    public void Deploy()
    {
        if(m_ScannedObject)
        {
            Debug.Log("Deploy Obj");
            var obj=Instantiate(m_ScannedObject.grantedObjectPrefab, transform.position + transform.up * m_DeployDistance, Quaternion.identity);
            if (obj.layer == LayerMask.NameToLayer( "Obstacle"))//TODO: better
            {
                var collider = obj.GetComponent<Collider2D>();
                if(collider)
                     AstarPath.active.UpdateGraphs(collider.bounds);
            }
        }
    }
}