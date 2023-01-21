using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPooling : SpawnPooling
{
    public static BulletSpawnPooling Instance { get; private set; }
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private GameObject playerRef;
    [SerializeField] private GameObject pointerRef;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateSpawnList(bulletPrefab);
    }

    // 
    public override void SetSpawnLocation(GameObject obj)
    {
        obj.transform.position = pointerRef.transform.position;
        obj.transform.rotation = playerRef.transform.rotation;
    }
}
