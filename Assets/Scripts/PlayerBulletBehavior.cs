using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float xPosLimit = 10f;
    private float yPostLimit = 6f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DespawnWhenOutOfBounds();
    }

    // Move the bullet in it's local up direction at _speed
    void Move()
    {
        transform.localPosition += transform.up * _speed * Time.deltaTime;
    }

    // Despawn self and readd to the object pooling
    void DespawnSelf()
    {
        BulletSpawnPooling.instance.DespawnObject(gameObject);
    }

    // Despawn the bullet if it goes out of bounds
    void DespawnWhenOutOfBounds()
    {
        if (transform.position.x > xPosLimit) DespawnSelf();
        if (transform.position.x < -xPosLimit) DespawnSelf();
        if (transform.position.y > yPostLimit) DespawnSelf();
        if (transform.position.y < -yPostLimit) DespawnSelf();
    }
}
