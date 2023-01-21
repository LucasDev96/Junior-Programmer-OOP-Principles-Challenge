using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPooling : SpawnPooling
{
    public EnemySpawnPooling Instance { get; private set; }

    [Header("Spawn List")]
    [SerializeField] private GameObject[] _enemies;
    private List<GameObject>[] _enemyLists;

    [Header("Spawn Position Range")]
    [SerializeField] private float topBottomXRange;
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float leftRightYRange;
    [SerializeField] private float leftXPosition;
    [SerializeField] private float rightXPosition;

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
        PickSideToSpawn(obj);
    }

    void PickSideToSpawn(GameObject obj)
    {
        int num = Random.Range(0, 4);

        switch (num)
        {
            case 0:
                SpawnLeftSide(obj);
                break;
            case 1:
                SpawnRightSide(obj);
                break;
            case 2:
                SpawnTopSide(obj);
                break;
            case 3:
                SpawnBottomSide(obj);
                break;
            default:
                SpawnBottomSide(obj);
                break;
        }
    }

    void SpawnLeftSide(GameObject obj)
    {
        float xPos = leftXPosition;
        float yPos = Random.Range(-leftRightYRange, leftRightYRange);

        obj.transform.position = new Vector3(xPos, yPos, 0);
    }

    void SpawnRightSide(GameObject obj)
    {
        float xPos = rightXPosition;
        float yPos = Random.Range(-leftRightYRange, leftRightYRange);

        obj.transform.position = new Vector3(xPos, yPos, 0);
    }

    void SpawnTopSide(GameObject obj)
    {
        float xPos = Random.Range(-topBottomXRange, topBottomXRange);
        float yPos = topYPosition;

        obj.transform.position = new Vector3(xPos, yPos, 0);
    }

    void SpawnBottomSide(GameObject obj)
    {
        float xPos = Random.Range(-topBottomXRange, topBottomXRange);
        float yPos = bottomYPosition;

        obj.transform.position = new Vector3(xPos, yPos, 0);
    }
}
