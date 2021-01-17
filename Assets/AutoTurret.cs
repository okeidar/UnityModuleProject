using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    [SerializeField] Transform[] directions;
    [SerializeField] float interval = 3f;

    Shooter m_shooter;
    bool m_canShoot = true;

    private void Start()
    {
        m_shooter = GetComponent<Shooter>();
    }
    private void Update()
    {
        if (m_canShoot)
        {
            System.Array.ForEach(directions, dir => m_shooter.ForceShootAt(dir));

            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        m_canShoot = false;
        yield return new WaitForSeconds(interval);
        m_canShoot = true;
    }
}
