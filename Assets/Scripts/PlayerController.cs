using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameUIManager.Instance.isGameActive | GameUIManager.Instance.isGamePaused) return; // stop update if game is paused or over
        LeftClick(); // move pointer and shoot
    }

    // Turn the player towards the cursor on click
    void TurnToMouseClick()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);

    }

    // Spawn a bullet after the player left clicks
    void ShootBullet()
    {
        BulletSpawnPooling.Instance.SetSpawnLocation(
            BulletSpawnPooling.Instance.SpawnObject());

        BulletSpawnPooling.Instance.PlayShootingSound();
    }

    // Activate other methods on left click
    void LeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TurnToMouseClick();
            ShootBullet();
        }
    }
}
