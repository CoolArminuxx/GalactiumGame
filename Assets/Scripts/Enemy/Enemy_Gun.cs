using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gun : MonoBehaviour
{
    private float timer;
    private AudioSource LaserSound;
    private GameObject Player;
    public float ShootDelay;
    public Color BulletColour;

    private void Start()
    {
        LaserSound = GetComponentInChildren<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");

    }
    public void Update()
    {
        ShootCheck();
    }

    public void ShootCheck()
    {
        if (Player.activeSelf == true)
        {
            timer += Time.deltaTime;

            if (timer > ShootDelay)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        GameObject spawnedBullet = Pool.Instance.GetEnemyBullet();
        spawnedBullet.GetComponent<SpriteRenderer>().color = BulletColour;
        ParticleSystem ps = spawnedBullet.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        psmain.startColor = BulletColour;

        if (spawnedBullet != null)
        {
            if (LaserSound != null)
            {
                LaserSound.Play();
            }
            spawnedBullet.transform.position = transform.position;
            spawnedBullet.SetActive(true);
            spawnedBullet.GetComponent<Enemy_Bullet>().direction = transform.right;
        }
    }
}
