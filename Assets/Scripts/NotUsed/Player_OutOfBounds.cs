using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_OutOfBounds : MonoBehaviour
{
    Rigidbody2D rb;
    ScreenBounds screenBounds;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBounds = FindObjectOfType<ScreenBounds>();
    }

    private void Update()
    {
        OutOfBoundsLogic();
    }

    private void OutOfBoundsLogic()
    {
        Vector3 newVelocity = rb.velocity;
        Vector3 tempPosition = transform.localPosition + newVelocity * Time.deltaTime;
        if (screenBounds.AmIOutOfBounds(tempPosition))
        {
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(tempPosition);
            transform.localPosition = newPosition;
        }
        else
        {
            rb.MovePosition(tempPosition);
        }
    }
}
