using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private ScannableObject m_ScannedObject;
    private Scannable m_ObectToScanInRange;
    private Scannable m_PreviewScanned;
    [SerializeField] GameObject m_DeployedParent;
    [SerializeField] float m_MinDeployDistance = 0.5f;
    [SerializeField] float m_MaxDeployDistance = 2f;
    [SerializeField] bool m_ShouldDeployOnMouse = true;
    private Transform m_MouseTransform;
 
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
        if(m_ScannedObject && m_PreviewScanned)
        {
            if(!m_PreviewScanned.IsDeployable)
            {
                Destroy(m_PreviewScanned.gameObject);
            }
            else
            {
                m_PreviewScanned.gameObject.transform.parent = m_DeployedParent.transform;
                m_PreviewScanned.Deploy();
                if (m_PreviewScanned.gameObject.layer == LayerMask.NameToLayer( "Obstacle"))//TODO: better
                {
                    var collider = m_PreviewScanned.gameObject.GetComponent<Collider2D>();
                    if(collider)
                        AstarPath.active.UpdateGraphs(collider.bounds);
                }
            }
            m_PreviewScanned = null;
        }
    }

    public void StartPreview(Vector2 mouseLocation)
    {
        if(m_ScannedObject && !m_PreviewScanned)
        {
            var objToDeploy = Instantiate(m_ScannedObject.grantedObjectPrefab, GetPreviewPosition(mouseLocation), Quaternion.identity);
            objToDeploy.transform.parent = m_DeployedParent.transform;
            m_PreviewScanned = objToDeploy.GetComponent<Scannable>();
            m_PreviewScanned.OnTryDelpoy();
        }
    }
    
    public void UpdatePreview(Vector2 mouseLocation)
    {
        if(m_PreviewScanned)
        {
            m_PreviewScanned.gameObject.transform.position = GetPreviewPosition(mouseLocation);
            m_PreviewScanned.OnDeployPreview();
        }
    }

    private Vector2 GetPreviewPosition(Vector2 mouseLocation)
    {
        if(!m_ShouldDeployOnMouse)
        {
            return transform.position + transform.up * m_MaxDeployDistance;
        }
        var distance = (mouseLocation - new Vector2(transform.position.x, transform.position.y)).sqrMagnitude;
        distance = Mathf.Clamp(distance, m_MinDeployDistance, m_MaxDeployDistance);
        return transform.position + transform.up * distance;
    }
}