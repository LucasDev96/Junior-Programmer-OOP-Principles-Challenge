using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager Instance { get; private set; }
    [SerializeField] private GameObject _healthBarPrefab;

    private void Awake()
    {
        Instance = this;
    }

    // Instantiate a healthbar, giving it an enemy reference, an offset, and an hp value
    // Returns itself so the enemy can get a ref to it
    public void CreateHealthBar(GameObject target, float yOffset, int healthValue)
    {
        GameObject temp = Instantiate(_healthBarPrefab);
        temp.GetComponent<HealthBarBehavior>().enemyRef = target; // assign enemy
        temp.GetComponent<HealthBarBehavior>().SetYOffset(yOffset); // assign y offset
        temp.GetComponent<HealthBarBehavior>().maxHealth = healthValue; // set max health
    }
}
