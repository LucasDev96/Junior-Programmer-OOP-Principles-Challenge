using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    [SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Move the bullet in it's local up direction at _speed
    void Move()
    {
        transform.localPosition += transform.up * _speed * Time.deltaTime;
    }
}
