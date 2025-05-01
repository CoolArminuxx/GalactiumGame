using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    private GameObject player;
    private Player_Health ph;
    private Player_Degradation pd;
    private Player_Powerups pp;

    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public Vector2 velocity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ph = player.GetComponent<Player_Health>();
        pd = player.GetComponent<Player_Degradation>();
        pp = player.GetComponent<Player_Powerups>();
    }

    private void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        BulletTrajectory();
    }

    private void BulletTrajectory()
    {
        Vector2 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
        float rotate = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotate - 180f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LeftWing"))
        {
            GameObject hitFlash = Pool.Instance.GetHitFlash();
            if (pd.lVal < 1 && hitFlash != null)
            {
                hitFlash.transform.position = other.transform.position;
                hitFlash.SetActive(true);
                if (pp.ShieldActive == false)
                {
                    ph.DamagePlayer();
                    pd.DamageLeftWing();
                }
                gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("RightWing"))
        {
            GameObject hitFlash = Pool.Instance.GetHitFlash();
            if (pd.rVal < 1 && hitFlash != null)
            {
                hitFlash.transform.position = other.transform.position;
                hitFlash.SetActive(true);
                if (pp.ShieldActive == false)
                {
                    ph.DamagePlayer();
                    pd.DamageRightWing();
                }
                gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Body"))
        {
            GameObject hitFlash = Pool.Instance.GetHitFlash();
            if (pd.bVal < 1 && hitFlash != null)
            {
                hitFlash.transform.position = other.transform.position;
                hitFlash.SetActive(true);
                if (pp.ShieldActive == false)
                {
                    ph.DamagePlayer();
                    pd.DamageBody();
                }
                gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }
}
