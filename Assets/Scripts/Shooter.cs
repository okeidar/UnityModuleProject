using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IShooter
{
    [SerializeField] float range = 5f;
    [SerializeField] float fireRate = .2f;
    [SerializeField] GameObject bulletPrefab;

    public float Range => range;
    public float FireRate => fireRate;

    bool m_canShoot=true;
    Rigidbody2D m_owner;

    private void Start()
    {
        m_owner = GetComponent<Rigidbody2D>();
    }

    public void ShootAt(Transform target)
    {
       if(m_canShoot)
        {
            Vector2 direction = ((Vector2)target.position - m_owner.position).normalized;
            Instantiate(bulletPrefab, m_owner.position, transform.rotation);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        m_canShoot = false;
        yield return new WaitForSeconds(fireRate);
        m_canShoot = true;
    }
}
