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

    // Start is called before the first frame update
    void Start()
    {
        m_controlledBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        RotatePlayer(m_mouseLocation);
        Move(m_movementInput);
        Shoot(m_isShootButtonPressed);


    }

    private void GetPlayerInput()
    {
        m_movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_mouseLocation =Camera.main.ScreenToWorldPoint( Input.mousePosition);
        m_isShootButtonPressed = Input.GetButtonDown("Fire1");
    }

    void Move(Vector2 movement) //TODO:should be in a movement interface
    {       
        Vector2 normalizedMovement = movement * playerSpeed * Time.deltaTime;
        m_controlledBody.MovePosition(m_controlledBody.position + normalizedMovement);
    }

    void RotatePlayer(Vector2 direction)//TODO:should be in a movement interface
    {
        Vector3 lookAt = direction - m_controlledBody.position;
        float currentAngle = m_controlledBody.rotation;
        float targetAngle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90f;
        float angleDiff = targetAngle - currentAngle;
        angleDiff = Mathf.Repeat(angleDiff + 180f, 360f) - 180f;
        targetAngle = currentAngle + angleDiff;
        m_controlledBody.MoveRotation( targetAngle);
    }

    void Shoot(bool isShootButtonPressed) // TODO: should be in an interface
    {
        if(isShootButtonPressed)
        {
           var bullet= Instantiate(basicBullet, m_controlledBody.position, transform.rotation);
            m_isShootButtonPressed = false;
        }
    }
}
