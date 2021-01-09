using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] GameObject basicBullet;

    private Rigidbody2D m_controlledBody;
    private Vector2 m_movementInput;
    private Vector2 m_mouseLocation;
    private bool m_isShootButtonPressed = false;
    private bool m_isScanButtonPressed = false;
    private bool m_isDeployButtonPressed = false;
    private Scanner m_Scanner;
    Weapon weapon;
    private int ammo = 0; //TODO: where to handle?

    // Start is called before the first frame update
    void Start()
    {
        m_controlledBody = GetComponent<Rigidbody2D>();
        m_Scanner = GetComponent<Scanner>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        RotatePlayer(m_mouseLocation);
        Move(m_movementInput);
        Shoot(m_isShootButtonPressed);
        Scan(m_isScanButtonPressed);
        DeployScan(m_isDeployButtonPressed);
    }

    private void GetPlayerInput()
    {
        m_movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_mouseLocation =Camera.main.ScreenToWorldPoint( Input.mousePosition);
        m_isShootButtonPressed = Input.GetButtonDown("Fire1");
        m_isScanButtonPressed = Input.GetButtonDown("Fire2");
        m_isDeployButtonPressed = Input.GetButtonDown("Fire3");
    }

    void Move(Vector2 movement) //TODO:should be in a movement interface
    {       
        Vector2 normalizedMovement = movement * playerSpeed * Time.deltaTime;
        m_controlledBody.MovePosition(m_controlledBody.position + normalizedMovement);
    }

    void RotatePlayer(Vector2 direction)//TODO:should be in a movement interface
    {
        Vector3 lookAt = direction - m_controlledBody.position;
        float targetAngle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90f;
    
        m_controlledBody.MoveRotation( targetAngle);
    }

    void Shoot(bool isShootButtonPressed) // TODO: should be in an interface
    {
        if(isShootButtonPressed)
        {
            if (weapon == null)
            {
                var bullet = Instantiate(basicBullet, m_controlledBody.position, transform.rotation);               
            }
            else
            {
                Instantiate(weapon.bulletPrefab, m_controlledBody.position, transform.rotation);
                ammo--;

                if(ammo<=0)
                {
                    weapon = null;
                }
            }
            m_isShootButtonPressed = false;
        }
    }

    void Scan(bool isScanPressed)
    {
        if (isScanPressed)
        {
            m_Scanner.Scan();
        }
    }

    void DeployScan(bool isDeployedPressed)
    {
        if (isDeployedPressed)
        {
            m_Scanner.Deploy();
        }
    }

    public void GrantWeapon(Weapon item)//TODO: should be more general
    {
        weapon = item;
        ammo = item.ammo;
    }
}
