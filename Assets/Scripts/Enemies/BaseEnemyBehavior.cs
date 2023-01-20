using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyBehavior : MonoBehaviour
{
    private GameObject _playerRef;
    [SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _playerRef = GameObject.FindWithTag("Player");
        RotateTowardsPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    // Move the enemy towards the player at a constant speed
    void MoveToPlayer()
    {
        transform.localPosition += transform.up * _speed * Time.deltaTime;
    }

    // Rotate the enemy to face the player
    void RotateTowardsPlayer()
    {
        Vector2 direction = _playerRef.transform.position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);

        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
