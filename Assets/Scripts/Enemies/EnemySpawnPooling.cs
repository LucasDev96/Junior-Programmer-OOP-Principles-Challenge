using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnPooling : SpawnPooling
{
    public static EnemySpawnPooling Instance { get; private set; }

    private GameObject _playerRef;

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
        _playerRef = GameObject.FindWithTag("Player");
        _enemyLists = new List<GameObject>[_enemies.Length];

        PopulateSpawnList(_enemies, _enemyLists);
    }

    // Get random enemy type, get enemy from array of enemy lists,
    // set to active and then remove from list
    public override GameObject SpawnObject()
    {
        int enemyType = Random.Range(0, _enemies.Length);
        GameObject temp = _enemyLists[enemyType].First();
        temp.SetActive(true);
        _enemyLists[enemyType].Remove(temp);

        return temp;
    }

    // Get type of enemy that needs to be despawn, deactivate it and
    // add it back to the appropriate enemy list
    public override void DespawnObject(GameObject obj)
    {
        if (!obj.CompareTag("Enemy"))
        {
            Debug.Log("Trying to despawn non enemy");
            return;
        }

        obj.SetActive(false);

        NormalEnemy enemy = obj.GetComponent<NormalEnemy>();
        int enemyType = enemy.enemyID;

        _enemyLists[enemyType].Add(obj);
    }

    public override void SetSpawnLocation(GameObject obj)
    {
        if (!obj.CompareTag("Enemy"))
        {
            Debug.Log("Trying to change location of non enemy");
            return;
        }

        PickSideToSpawn(obj);
        obj.GetComponent<NormalEnemy>().RotateTowardsPlayer(_playerRef);
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
