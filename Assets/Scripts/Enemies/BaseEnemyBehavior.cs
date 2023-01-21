using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseEnemyBehavior : MonoBehaviour
{
    private GameObject _UIManager;
    [SerializeField] private float _speed;
    [field: SerializeField] public int enemyID { get; private set; }
    [SerializeField] private int _maxEnemyHealth;
    private int _health;


    // Start is called before the first frame update
    void Start()
    {
        _UIManager = GameObject.FindGameObjectWithTag("GameUIManager");
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    // Move the enemy towards the player at a constant speed
    protected void MoveToPlayer()
    {
        transform.localPosition += transform.up * _speed * Time.deltaTime;
    }

    // Rotate the enemy to face the player
    public void RotateTowardsPlayer(GameObject player)
    {
        Vector2 direction = player.transform.position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage(collision);
        HurtPlayer(collision);
    }

    // Take damage when hit by a bullet, and despawn object if health is 0
    protected void TakeDamage(Collider2D bullet)
    {
        if (bullet.CompareTag("Bullet"))
        {
            BulletSpawnPooling.Instance.DespawnObject(bullet.gameObject);

            _health--;

            if (_health <= 0)
            {
                DespawnSelf();
                ResetHealth(); // reset health back to max after despawn
            }
        }
    }

    // Remove a life when coming in contact with the player, also despawning self
    protected void HurtPlayer(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            _UIManager.GetComponent<GameUIManager>().SetLivesText();
            DespawnSelf();
        }
    }

    // Reset health back to max
    void ResetHealth() { _health = _maxEnemyHealth; }

    void DespawnSelf()
    {
        EnemySpawnPooling.Instance.DespawnObject(gameObject);
    }
}
