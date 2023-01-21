using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SpawnPooling : MonoBehaviour
{
    [SerializeField] protected int _spawnCount;
    protected List<GameObject> _spawnList;

    // Add _spawnCount amount of gameobjects to the _spawnList;
    protected virtual void PopulateSpawnList(GameObject obj)
    {
        _spawnList = new List<GameObject>();

        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject temp = Instantiate(obj);
            temp.SetActive(false);
            _spawnList.Add(temp);
        }
    }

    // Add _spawnCount amount of gameobjects to a given array of lists
    protected virtual void PopulateSpawnList(GameObject obj, List<GameObject>[] lists)
    {
        for (int i = 0; i < lists.Length; i++)
        {
            lists[i] = new List<GameObject>();

            for (int j = 0; j < _spawnCount; j++)
            {
                GameObject temp = Instantiate(obj);
                temp.SetActive(false);
                _spawnList.Add(temp);
            }
        }
    }

    // Each child class should define their own spawn location
    public abstract void SetSpawnLocation(GameObject obj);

    // Set object to active and remove from _spawnList
    public GameObject SpawnObject()
    {
        GameObject temp = _spawnList.First();
        temp.SetActive(true);
        _spawnList.Remove(_spawnList.First());
        return temp;
    }

    // Set object to inactive and put back into the _spawnList
    public void DespawnObject(GameObject obj)
    {
        obj.SetActive(false);
        _spawnList.Add(obj);
    }
}
