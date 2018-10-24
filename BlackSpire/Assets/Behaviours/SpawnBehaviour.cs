using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// playerPrefab, enemyPrefabs, towerPrefabs, bullet-pool

public class SpawnBehaviour : MonoBehaviour {
    [SerializeField]GameObject m_DefaultPrefab;
    [SerializeField] int m_PoolSize = 0;

    GameObject[] objectPool;
    int poolIndex = 0;

    // --- UNITY UPDATES ---

    void OnEnable () {
        if (m_PoolSize > 0)
            if (objectPool == null || objectPool.Length != m_PoolSize) InitializePool();
    }

    // --- PUBLIC ---

    // Unused, placeholder
    public void SpawnAt(Vector3 position, bool pooled = false)
    {
        GameObject instance = Instantiate(m_DefaultPrefab);
        instance.transform.position = position;
        MovementBehaviour movement = m_DefaultPrefab.GetComponent<MovementBehaviour>();
    }

    // --- PRIVATE ---

    void InitializePool() { objectPool = new GameObject[m_PoolSize]; }
}
