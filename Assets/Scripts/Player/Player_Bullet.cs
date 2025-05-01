using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    private Enemy_Health healthRef;
    public Vector2 direction = new Vector2(1, 0);
    private float speed = 8;
    private Vector2 velocity;

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
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject hitFlash = Pool.Instance.GetHitFlash();
            if (hitFlash != null)
            {
                hitFlash.transform.position = other.transform.position;
                hitFlash.SetActive(true);
            }
            healthRef = other.GetComponent<Enemy_Health>();
            healthRef.DamageEnemy();
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }
}
