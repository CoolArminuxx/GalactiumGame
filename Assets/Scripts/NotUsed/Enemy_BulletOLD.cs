using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BulletOLD : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    private Player_Health ph;
    private Player_Degradation pd;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        ph = player.GetComponent<Player_Health>();
        pd = player.GetComponent<Player_Degradation>();

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotate = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotate - 180f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LeftWing"))
        {
            Debug.Log("LEFT");
            ph.DamagePlayer();
            pd.DamageLeftWing();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("RightWing"))
        {
            Debug.Log("RIGHT");
            ph.DamagePlayer();
            pd.DamageRightWing();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Body"))
        {
            Debug.Log("BODY");
            ph.DamagePlayer();
            pd.DamageBody();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
