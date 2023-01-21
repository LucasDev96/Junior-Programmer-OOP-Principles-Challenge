using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPooling : SpawnPooling
{
    public EnemySpawnPooling Instance { get; private set; }

    [SerializeField] private GameObject[] _enemies;
    private List<GameObject>[] _enemyLists;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        _enemyLists = new List<GameObject>[_enemies.Length];

        PopulateSpawnList(_enemies, _enemyLists);
    }

    public override void SetSpawnLocation(GameObject obj)
    {
        throw new System.NotImplementedException();
    }
}
