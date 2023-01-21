using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private float _spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(_spawnTime);

        EnemySpawnPooling.Instance.SetSpawnLocation(
            EnemySpawnPooling.Instance.SpawnObject());

        StartCoroutine(SpawnEnemy());
    }
}
