using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShootOLD : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    public int angle;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            shoot();
        }
    }

    void shoot()
    {
        angle = 0;
        for(int i = 0; i < 3; i++)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
            angle += 20;
        }
    }
}
