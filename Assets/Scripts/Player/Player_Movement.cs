using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float maxSpeed = 5f;
    private float originalSpeed;
    private Vector2 moveVector = Vector2.zero;
    private PlayerActions controls = null;
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private AudioSource DashAudio;
    [SerializeField] private GameObject DashEffect;
    ScreenBounds screenBounds;

    private float dashSpeed;
    private float dashLength = .5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;
    private bool canDash;

    private void Awake()
    {
        controls = new PlayerActions();
        screenBounds = FindObjectOfType<ScreenBounds>();
    }
    private void OnEnable()
    {
        controls.Enable();
        controls.Gameplay.Move.performed += Movement;
        controls.Gameplay.Move.canceled += MovementCancelled;
    }
    private void OnDisable()
    {
        controls.Disable();
        controls.Gameplay.Move.performed += Movement;
        controls.Gameplay.Move.canceled += MovementCancelled;
    }
    private void Start()
    {
        Cursor.visible = false;
        SetSpeed();
        dashSpeed = speed + 3;
        canDash = true;
    }

    void Update()
    {
        if (Gamepad.current != null)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                Rb.velocity = transform.up * speed;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                Rb.velocity = -transform.up * speed;
            }
            else if (Input.GetAxis("Vertical") == 0)
            {
                Rb.velocity = transform.up * 0f;
            }
        }
        else
        {
            if (moveVector.y > 0)
            {
                Rb.velocity = transform.up * speed;
            }
            else if (moveVector.y < 0)
            {
                Rb.velocity = -transform.up * speed;
            }
            else if (moveVector.y == 0)
            {
                Rb.velocity = transform.up * 0f;
            }
        }
        OutOfBoundsLogic();
        Dash();
    }

    public void SetSpeed()
    {
        speed = maxSpeed;
    }

    private void Movement(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void MovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    private void OutOfBoundsLogic()
    {
        Vector3 tempPosition = transform.localPosition;
        if (screenBounds.AmIOutOfBounds(tempPosition))
        {
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(tempPosition);
            transform.localPosition = newPosition;
        }
    }

    private void Dash()
    {
        if (controls.Gameplay.Dash.ReadValue<float>() > 0 && (Rb.velocity.x > 0 || Rb.velocity.y > 0))
        {
            if (canDash == true)
            {
                canDash = false;
                DashEffect.SetActive(true);
                DashAudio.Play();
                originalSpeed = speed;
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    speed = dashSpeed;
                    dashCounter = dashLength;
                }
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                speed = originalSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;

            if (dashCoolCounter <= 0)
            {
                DashEffect.SetActive(false);
                canDash = true;
            }
        }
    }
}