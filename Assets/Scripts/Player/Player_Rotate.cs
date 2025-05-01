using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Rotate : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private GameObject Player;

    private float timer = 0f;
    private Sprite currentSprite;
    private string SpriteFrame;
    private int spriteInt;

    private PlayerActions controls = null;
    private Vector2 rotation = Vector2.zero;

    public float LeftSpeed;
    public float RightSpeed;

    private void Awake()
    {
        controls = new PlayerActions();
        controls.Gameplay.Rotate.performed += cntxt => rotation = cntxt.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += cntxt => rotation = Vector2.zero;
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    void Start()
    {
        Anim.SetFloat("Speed", 0f);
        LeftSpeed = 0.8f;
        RightSpeed = 0.8f;
    }

    void Update()
    {
        GetSpriteFrame();
        CheckRotation();
    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
    void CheckRotation()
    {
        if (rotation.x < 0)
        {
            Anim.SetFloat("Speed", LeftSpeed);
            timer += Time.deltaTime;
        }
        else if (rotation.x > 0)
        {
            Anim.SetFloat("Speed", -RightSpeed);
            timer += Time.deltaTime;
        }
        else if (rotation.x == 0)
        {
            Anim.SetFloat("Speed", 0f);
        }
    }
    void GetSpriteFrame()
    {
        currentSprite = GetComponent<SpriteRenderer>().sprite;
        SpriteFrame = currentSprite.name;
        string[] words = SpriteFrame.Split(' ');
        string lastPart = words[words.Length - 1];
        string num = lastPart.Remove(0, 3);
        int.TryParse(num, out spriteInt);
        Player.transform.eulerAngles = new Vector3(0, 0, (spriteInt * 10));
    }
}
