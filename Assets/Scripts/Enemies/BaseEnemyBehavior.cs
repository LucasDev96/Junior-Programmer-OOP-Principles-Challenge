using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseEnemyBehavior : MonoBehaviour
{
    private static GameObject _UIManager;
    [SerializeField] private int score;
    [SerializeField] private float _speed;
    [field: SerializeField] public int enemyID { get; private set; }
    [SerializeField] private int _maxEnemyHealth;
    private int _health;

    public GameObject healthBarRef { get; private set; }
    [SerializeField] private float _healthBarYOffset;


    private void Awake()
    {
        GetHealthBar();
        HideHealthBar();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUIManagerRef();
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
            healthBarRef.GetComponent<HealthBarBehavior>().currentHealth = _health;
            healthBarRef.GetComponent<HealthBarBehavior>().UpdateSlider();

            if (_health <= 0)
            {
                DespawnSelf();
                AddToScore();
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
            ResetHealth();
        }
    }

    // Add to the player's score on "death"
    protected void AddToScore()
    {
        _UIManager.GetComponent<GameUIManager>().SetScoreText(score);
    }

    // Reset health back to max
    void ResetHealth()
    {
        _health = _maxEnemyHealth;
        healthBarRef.GetComponent<HealthBarBehavior>().SetFullHealth();
    }

    void DespawnSelf()
    {
        HideHealthBar();
        EnemySpawnPooling.Instance.DespawnObject(gameObject);
    }

    void SetUIManagerRef()
    {
        if (_UIManager == null)
        {
            _UIManager = GameObject.FindGameObjectWithTag("GameUIManager");
        }
    }

    // For use on Start(), when the enemy is created assign it a healthbar
    void GetHealthBar()
    {
        healthBarRef = HealthBarManager.Instance
            .CreateHealthBar(gameObject, _healthBarYOffset, _maxEnemyHealth);
    }

    // Hide the health bar, for use when despawning the enemy
    void HideHealthBar()
    {
        healthBarRef.GetComponent<HealthBarBehavior>().HideHealthBar();
    }

    // Show the health bar, public so it can be called from the spawn pooler
    public void ShowHealthBar()
    {
        healthBarRef.GetComponent<HealthBarBehavior>().ShowHealthBar();
    }
}
