using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannableBall : Scannable
{
    protected override void Awake() 
    {
        base.Awake();
        m_DeployableColor = new Color(0,225,0,0.5f);
        m_NotDeployableColor = new Color(250,0,0,0.5f);
    }
}
