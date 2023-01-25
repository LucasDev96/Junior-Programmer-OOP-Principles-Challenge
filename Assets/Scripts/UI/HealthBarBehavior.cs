using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HealthBarBehavior : MonoBehaviour
{
    private GameObject _enemyRef;
    public GameObject enemyRef
    {
        get { return _enemyRef; }
        set { _enemyRef = SetEnemyRef(value); }
    }
    private float _yOffset;

    [SerializeField] private Image _healthSlider;

    private float _maxHealth;
    public float maxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = SetMaxHealth(value); }
    }

    private float _currentHealth;
    public float currentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = SetCurrentHealth(value); }
    }

    private void Update()
    {
        MoveWithTarget();
    }

    // Set the value for the slider back to full
    public void SetFullHealth() { _healthSlider.fillAmount = 1; }

    // Set the enemyRef value if the target is a valid enemy
    private GameObject SetEnemyRef(GameObject enemy)
    {
        if (!enemy.CompareTag("Enemy"))
        {
            Debug.Log("Trying to set non-enemy ref to health bar");
            return null;
        }
        return enemy;
    }

    // Set the max health if it's a whole number over 0
    private float SetMaxHealth(float health)
    {
        if (health % 1 == 0 && health > 0)
        {
            _currentHealth = health; // make sure current health starts the same as max
            return health;
        }


        Debug.Log("Trying to set max health value to invalid amount");
        return 0;
    }

    // Set the max health if it's a whole number over 0, equal to or less than max health
    private float SetCurrentHealth(float currentHealth)
    {
        if (currentHealth % 1 == 0 &&
            currentHealth > 0 &&
            currentHealth <= _maxHealth) return currentHealth;

        Debug.Log("Trying to set current health value to invald amount");
        return 0;
    }

    // Adjust the slider to match the current health
    public void UpdateSlider()
    {
        _healthSlider.fillAmount = _maxHealth / _currentHealth;
    }

    // Set the y offset value for how far above the enemy the healthbar should be
    public void SetYOffset(float y)
    {
        if (y > 0) _yOffset = y;
        Debug.Log("Y offset value is negative");
    }

    // Follow the target's location, with the addition of the y offset
    private void MoveWithTarget()
    {
        Vector3 enemyPos = _enemyRef.transform.position;
        transform.position = new Vector3(enemyPos.x, enemyPos.y + _yOffset, enemyPos.z);
    }
}
