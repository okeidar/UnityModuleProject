using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scannable : MonoBehaviour
{
    [SerializeField] ScannableObject grantedObect;
    private Collider2D m_collider;
    protected SpriteRenderer m_spriteRenderer;
    protected bool m_IsColliding;
    private Color m_initialColor;
    private bool m_initialTrigger;
    protected Color m_DeployableColor = new Color(0, 225, 0, 0.5f);
    protected Color m_NotDeployableColor = new Color(250, 0, 0, 0.5f);
    protected Color m_ScannedColor = new Color(0, 0, 250, 1);
    public bool IsPreview;
    public bool IsDeployable;

    protected virtual void Awake()
    {
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_collider = gameObject.GetComponent<Collider2D>();
        m_initialColor = m_spriteRenderer.color;
        m_initialTrigger = m_collider.isTrigger;
    }
    public void OnDeployPreview(float deployCompletedState)
    {
        IsDeployable = GetIsDeployable();
        if (IsPreview)
        {
            ShowDeployable(IsDeployable, deployCompletedState);
        }
    }
    protected virtual bool GetIsDeployable()
    {
        return !m_IsColliding;
    }
    protected virtual void ShowDeployable(bool deployable, float deployCompletedState)
    {
        var color = deployable ? m_DeployableColor : m_NotDeployableColor;
        color.a = deployCompletedState;
        m_spriteRenderer.color = color;
    }
    public virtual void OnDeploy()
    {
        IsPreview = false;
        m_collider.isTrigger = m_initialTrigger;
        m_spriteRenderer.color = m_initialColor;
    }
    public void OnScan(float scanCompletePerc)
    {
        m_spriteRenderer.color = Color.Lerp(m_ScannedColor, m_initialColor, scanCompletePerc);
    }
    public void OnScanStop()
    {
        m_spriteRenderer.color = m_initialColor;
    }
    public ScannableObject GetScannedObject()
    {
        return grantedObect;
    }
    public void OnPreviewStart()
    {
        IsPreview = true;
        m_collider.isTrigger = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        m_IsColliding = other.gameObject != null;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        m_IsColliding = false;
    }
}
