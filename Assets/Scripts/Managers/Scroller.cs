using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public Transform topPosition;
    public Rigidbody2D[] imageRb;
    public float movementSpeedY;
    public float movementSpeedX;
    public Transform outOfBounds;

    private void FixedUpdate()
    {
        TilesMovement();
    }

    private void TilesMovement()
    {
        for (int i = 0; i < imageRb.Length; i++)
        {
            imageRb[i].velocity = new Vector2(movementSpeedX, movementSpeedY) * Time.fixedDeltaTime;

            CheckPosition(imageRb[i].gameObject);
        }
    }

    public void CheckPosition(GameObject objectToCheck)
    {
        if (objectToCheck.transform.position.y <= outOfBounds.position.y)
        {
            MoveToTheTop(objectToCheck);
        }
    }

    public void MoveToTheTop(GameObject objectToMove)
    {
        objectToMove.transform.position = topPosition.position;
    }
}
