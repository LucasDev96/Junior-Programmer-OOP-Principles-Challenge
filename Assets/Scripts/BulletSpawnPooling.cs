using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPooling : SpawnPooling
{
    public static BulletSpawnPooling Instance { get; private set; }
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private GameObject playerRef;
    [SerializeField] private GameObject pointerRef;

    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateSpawnList(bulletPrefab);
        SetAudioSource();
    }

    // Set the bullet's position and rotation to be the same as the pointer
    public override void SetSpawnLocation(GameObject obj)
    {
        obj.transform.position = pointerRef.transform.position;
        obj.transform.rotation = playerRef.transform.rotation;
    }

    void SetAudioSource() { _audioSource = gameObject.GetComponent<AudioSource>(); }

    // Play a shooting sound when the bullet is shot
    public void PlayShootingSound() { _audioSource.Play(); }
}
