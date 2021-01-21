using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct EnemyInfo
{
    public GameObject _enemy;
    public int _weight;
}
public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject _target;

    [SerializeField] float _radiusMin = 10f;
    [SerializeField] float _radiusMax = 20f;
    [SerializeField] float _delay = 3f;
    [SerializeField] float _delayRandom = .5f;
    [SerializeField] float _distanceAllowed = 0.5f;
    [SerializeField] EnemyInfo[] _enemies;

    int m_weights;
    // Start is called before the first frame update
    void Start()
    {
        System.Array.ForEach(_enemies, e => m_weights += e._weight);
        Invoke("SpawnHandler", _delay);
    }


    void SpawnHandler()
    {
        var enemy = GetRandomEnemy();
        SpawnEnemy(enemy);
    }
    GameObject GetRandomEnemy()
    {
        int chance = Random.Range(0, m_weights + 1);

        foreach (EnemyInfo enemy in _enemies)
        {
            if ((chance -= enemy._weight) < 0)
            {
                return enemy._enemy;
            }
        }
        Debug.LogError("Chance failed");
        return null;
    }

    void SpawnEnemy(GameObject enemyToSpawn)
    {
        if (enemyToSpawn == null)
        {
            Invoke("SpawnHandler", 0.1f);
            return;
        }

        var spawnPos = RandomPointOnSphere();
        var nodeInfo = AstarPath.active.GetNearest(spawnPos);
        GraphNode node = nodeInfo.node;
        var nodeDiestance = ((Vector2)nodeInfo.position - spawnPos).magnitude;  
        if (node.Walkable && nodeDiestance <= _distanceAllowed)
        {
            Instantiate(enemyToSpawn, spawnPos, Quaternion.identity).GetComponent<EnemyAI>().target = _target.transform;//TODO: dont kill me, ill fix this
            Random.Range(_delay - _delayRandom, _delay + _delayRandom);
            Invoke("SpawnHandler", _delay);
        }
        else
        {
            Invoke("SpawnHandler", 0.1f);
        }
    }


    private Vector2 RandomPointOnSphere()
    {
        Vector2 subjectPos = _target.transform.position;
        Vector2 randomDirection = (Random.insideUnitCircle * subjectPos).normalized;


        float randomDistance = Random.Range(_radiusMax, _radiusMax);

        Vector2 randomLocation = randomDirection * randomDistance;
        Vector2 spawnPoint = (Vector2)_target.transform.position + randomLocation;

        return spawnPoint;
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        var pos = transform.position;
        if (_target)
        {
            pos = _target.transform.position;
        }

        Handles.color = Color.yellow;
        Handles.DrawWireDisc(pos, Vector3.forward, _radiusMin);
        Handles.color = Color.red;
        Handles.DrawWireDisc(pos, Vector3.forward, _radiusMax);
    }
#endif

}
