using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Assets.Scripts;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float speed = 7f;
    [SerializeField] float nextWaypointDistance = .3f;

    Path m_path;
    int m_currentWaypoint = 0;
    IShooter m_shooter;

    Seeker m_seeker;
    Rigidbody2D m_controlledEnemy;

    void Start()
    {
        m_seeker = GetComponent<Seeker>();
        m_controlledEnemy = GetComponent<Rigidbody2D>();
        m_shooter = GetComponent<IShooter>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (m_seeker.IsDone())
        {
            m_seeker.StartPath(m_controlledEnemy.position, target.position, StartMoving);
        }
    }

    void StartMoving(Path p)
    {
        if (!p.error)
        {
            m_path = p;
            m_currentWaypoint = 0;
        }
    }

    void Update() //TODO: states
    {
        if (Vector2.Distance(m_controlledEnemy.position, target.position) <= m_shooter.Range) //TODO: use LOS to move around obstacles
        {
            StopMoving();
            RotateEnemy(((Vector2)target.position - m_controlledEnemy.position).normalized);
            m_shooter.ShootAt(target);
            return;
        }

        MoveAimToTarget();
    }

    private void MoveAimToTarget()//TODO: in a movement script
    {
        if (m_path == null)
            return;

        if (m_currentWaypoint >= m_path.vectorPath.Count)
        {
            return;
        }      

        Vector2 direction = ((Vector2)m_path.vectorPath[m_currentWaypoint] - m_controlledEnemy.position).normalized;
        Vector2 movement = direction * speed * Time.deltaTime;

        m_controlledEnemy.MovePosition(m_controlledEnemy.position + movement);
        RotateEnemy(direction);

        if (Vector2.Distance(m_controlledEnemy.position, m_path.vectorPath[m_currentWaypoint]) < nextWaypointDistance)
        {
            m_currentWaypoint++;
        }
    }

    private void StopMoving()
    {
        m_controlledEnemy.velocity = new Vector2(0, 0);
    }

    void RotateEnemy(Vector2 direction)
    {
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        m_controlledEnemy.MoveRotation(targetAngle);
    }
}
