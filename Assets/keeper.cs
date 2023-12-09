using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keeper : MonoBehaviour
{
   
    public float speed = 2f; // Adjust the speed of the keeper
    public float movementRange = 2f; // Adjust the range of keeper's movement

    private Vector2 initialPosition;

    void Start()
    {
        initialPosition = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        MoveKeeper();
    }

    public void SetInitialPosition(float x)
    {
        initialPosition = new Vector2(x, transform.position.y);
    }

    public void MoveKeeper()
    {
        // Move the keeper in the XY plane using Mathf.Sin function within a specified range
        float keeperMovement = Mathf.Sin(Time.time * speed);
        float xPosition = initialPosition.x + keeperMovement * movementRange;

        // Update the keeper's position in the XY plane
        transform.position = new Vector3(xPosition, initialPosition.y, transform.position.z);
    }
}
