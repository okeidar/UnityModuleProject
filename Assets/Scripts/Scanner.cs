using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private ScannableObject m_ScannedObject;
    private Scannable m_ObectToScanInRange;
    private Scannable m_PreviewScanned;
    [SerializeField] float m_MinDeployDistance = 0.5f;
    [SerializeField] float m_MaxDeployDistance = 2f;
    [SerializeField] bool m_ShouldDeployOnMouse = true;
    [SerializeField] private float ScanTime = 1f;
    [SerializeField] private float DeployTime = 2f;
    [SerializeField] private float DeployChargeTime = 2f;
    private float m_timeLeftToScan;
    private float m_timeLeftToDeploy;
    private float m_ChargeTimeEnd;
    private Collider2D m_Collider;
    private SpriteRenderer m_SR;
    


    private Transform m_MouseTransform;
    private void Start()
    {
        m_Collider = gameObject.GetComponent<Collider2D>();
        m_SR = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(!m_ObectToScanInRange)
        {
            var scannable = other.GetComponent<Scannable>();
            if(scannable)
            {
                m_ObectToScanInRange = scannable;
                m_timeLeftToScan = ScanTime;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        var scannable = other.GetComponent<Scannable>();
        if(m_ObectToScanInRange && scannable == m_ObectToScanInRange)
        {
            m_ObectToScanInRange.OnScanStop();
            m_ObectToScanInRange = null;
        }
    }
    public void StopScan()
    {
        ActivateScanner(false);
        if(m_ObectToScanInRange)
        {
            m_timeLeftToScan = ScanTime;
            m_ObectToScanInRange.OnScanStop();
        }
    }
    public ScannableObject Scan()
    {
        ActivateScanner(true);
        if (m_ObectToScanInRange)
        {
            //continue scanning
            if(m_timeLeftToScan > 0)
            {
                var scanCompletePerc = ScanTime > 0 ? (ScanTime- m_timeLeftToScan)/ScanTime : 1;
                m_ObectToScanInRange.OnScan(scanCompletePerc);
                m_timeLeftToScan -= Time.deltaTime;
            }
            //finish scanning
            else
            {
                m_ObectToScanInRange.OnScanStop();
                m_ScannedObject = m_ObectToScanInRange.GetScannedObject();
                return m_ScannedObject;
            }
        }
        return null;
    }

    public void Deploy()
    {
        ActivateScanner(false);
        if(m_PreviewScanned)
        {
            //wont deploy
            if(!m_PreviewScanned.IsDeployable || m_timeLeftToDeploy > 0)
            {
                Destroy(m_PreviewScanned.gameObject);
            }
            //deploy
            else
            {
                m_PreviewScanned.OnDeploy();
                m_PreviewScanned.gameObject.transform.SetParent(null);

                m_ChargeTimeEnd = Time.time + DeployChargeTime;
                if (m_PreviewScanned.gameObject.layer == LayerMask.NameToLayer( "Obstacle"))//TODO: better
                {
                    var collider = m_PreviewScanned.gameObject.GetComponent<Collider2D>();
                    if(collider)
                    {
                        AstarPath.active.UpdateGraphs(collider.bounds);
                    }
                }
            }
            m_PreviewScanned = null;
        }
    }

    public void StartPreview(Vector2 mouseLocation)
    {
        if(m_ScannedObject && !m_PreviewScanned && m_ChargeTimeEnd < Time.time)
        {
            var objToDeploy = Instantiate(m_ScannedObject.grantedObjectPrefab, GetPreviewPosition(mouseLocation), Quaternion.identity);
            m_PreviewScanned = objToDeploy.GetComponent<Scannable>();
            m_PreviewScanned.gameObject.transform.SetParent(gameObject.transform);
            m_PreviewScanned.OnPreviewStart();
            m_timeLeftToDeploy = DeployTime;
            Debug.Log("startPreview");
        }
    }
    
    public void UpdatePreview(Vector2 mouseLocation)
    {
        if(m_PreviewScanned)
        {
            if(m_timeLeftToDeploy > 0)
            {
                m_timeLeftToDeploy -= Time.deltaTime;
                m_timeLeftToDeploy = Mathf.Max(m_timeLeftToDeploy, 0);
            }
            var deployCompletePerc = DeployTime > 0 ? (DeployTime- m_timeLeftToDeploy)/DeployTime : 1;
            m_PreviewScanned.gameObject.transform.position = GetPreviewPosition(mouseLocation);
            m_PreviewScanned.OnDeployPreview(deployCompletePerc);
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
    public void ActivateScanner(bool activate)
    {
        if(activate != m_Collider.enabled || activate != m_SR.enabled)
        {
            m_Collider.enabled = activate;
            m_SR.enabled = activate;
        }
    }
}