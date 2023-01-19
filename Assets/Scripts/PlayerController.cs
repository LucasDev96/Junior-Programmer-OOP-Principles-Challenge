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
        TurnToMouseClick();
    }

    // Turn the player towards the cursor on click
    void TurnToMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = mousePosition - transform.position;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
