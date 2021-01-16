using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] GameObject basicBullet;
    [SerializeField] private Scanner m_Scanner;
    [SerializeField] private HealthObject m_Health;

    private Rigidbody2D m_controlledBody;
    private Vector2 m_movementInput;
    private Vector2 m_mouseLocation;
    private bool m_isShootButtonPressed = false;
    private bool m_isScanButtonHold = false;
    private bool m_isScanButtonRelease = false;
    private bool m_isDeployButtonPressed = false;
    private bool m_isDeployButtonReleased = false;
    private bool m_isDeployButtonHold =false;
    Weapon weapon;
    private Pickable m_objectInHand;
    private int ammo = 0; //TODO: where to handle?
    // Start is called before the first frame update
    void Start()
    {
        m_controlledBody = GetComponent<Rigidbody2D>();
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        RotatePlayer(m_mouseLocation);
        Shoot(m_isShootButtonPressed);
        Scan(m_isScanButtonHold, m_isScanButtonRelease);
        DeployScan(m_isDeployButtonPressed, m_isDeployButtonReleased, m_isDeployButtonHold);
    }
    private void FixedUpdate()
    {
        Move(m_movementInput);

    }

    private void InitUI()
    {
        UIManager.Instance.SetDefaultWeapon();
        UIManager.Instance.SetLife(m_Health.CurrentHealth);
        UIManager.Instance.SetItemInHand(null);
        UIManager.Instance.SetScannedItem(null);
    }

    private void GetPlayerInput()
    {
        m_movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_mouseLocation =Camera.main.ScreenToWorldPoint( Input.mousePosition);
        m_isShootButtonPressed = Input.GetButtonDown("Fire1");
        m_isScanButtonHold = Input.GetButton("Fire2");
        m_isScanButtonRelease = Input.GetButtonUp("Fire2");
        m_isDeployButtonPressed = Input.GetButtonDown("Fire3");
        m_isDeployButtonHold = Input.GetButton("Fire3");
        m_isDeployButtonReleased = Input.GetButtonUp("Fire3");
    
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
                UIManager.Instance.SetAmmo(ammo);
                if(ammo<=0)
                {
                    weapon = null;
                    UIManager.Instance.SetDefaultWeapon();
                }
            }
            m_isShootButtonPressed = false;
        }
    }

    void Scan(bool isScanHeld, bool isScanReleased)
    {
        if (isScanHeld)
        {
            var item = m_Scanner.Scan();
            if(item != null)
            {
                UIManager.Instance.SetScannedItem(item.Icon);
            }
        } 
        if(isScanReleased)
        {
            m_Scanner.StopScan();
        }
    }

    void DeployScan(bool isDeployedPressed, bool isDeployedReleased, bool isDeployPreview)
    {
        if (isDeployedReleased)
        {
            m_Scanner.Deploy();
        } 
        else
        if (isDeployedPressed)
        {
            m_Scanner.StartPreview(m_mouseLocation);
        }
        if(isDeployPreview)
        {
            m_Scanner.UpdatePreview(m_mouseLocation);
        }
    }

    private void UpdateObjectInHand()
    {
        if (m_objectInHand is Weapon)
        {
            weapon = (Weapon)m_objectInHand;
            ammo = weapon.ammo;
            UIManager.Instance.SetNewWeapon(m_objectInHand.Icon, ammo);
        }
        UIManager.Instance.SetItemInHand(m_objectInHand.Icon);
    }

    private void DropObjectInHand()
    {
        if(m_objectInHand is Weapon)
        {
            weapon = null;
            ammo = 0;
            UIManager.Instance.SetDefaultWeapon();
        }
    }

    public void TakeItem(Pickable item)
    {
        Debug.Log($"Player took {item.name}");
        if(m_objectInHand != null)
        {
            DropObjectInHand();
        }
        m_objectInHand = item;
        UpdateObjectInHand();
    }

    public Pickable GetObjectInHand()
    {
        return m_objectInHand;
    }

    public void UpdateLife(int life)
    {
        UIManager.Instance.SetLife(life);
    }

    public void Kill()
    {
        UIManager.Instance.GameOver();
        Debug.Log("GameOver");
    }
}
