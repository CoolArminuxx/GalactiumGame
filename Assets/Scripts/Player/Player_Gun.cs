using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Gun : MonoBehaviour
{
    [SerializeField] private GameObject[] Muzzles;
    [SerializeField] private AudioSource LaserAudio;
    private PlayerActions controls = null;
    private bool Shooting = true;
    private float timer;
    public float shootDelay = 1;

    private void Awake()
    {
        controls = new PlayerActions();
    }
    public void Update()
    {
        CheckCanShoot();
    }
    private void CheckCanShoot()
    {
        if (controls.Gameplay.Fire.ReadValue<float>() > 0)
        {
            if (Shooting == true)
            {
                StartCoroutine(Shoot());
                Shooting = false;
            }
        }
    }

    public IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.1f / shootDelay);
        for (int i = 0; i < Muzzles.Length; i++)
        {
            GameObject spawnedBullet = Pool.Instance.GetPlayerBullet();
            GameObject spawnedMuzzleFlash = Pool.Instance.GetMuzzleFlash();

            if (spawnedBullet != null)
            {
                spawnedMuzzleFlash.transform.position = Muzzles[i].transform.position;
                spawnedMuzzleFlash.GetComponent<MuzzleFollow>().FollowObject = Muzzles[i];
                spawnedMuzzleFlash.SetActive(true);

                spawnedBullet.transform.position = Muzzles[i].transform.position;
                spawnedBullet.SetActive(true);
                spawnedBullet.GetComponent<Player_Bullet>().direction = Muzzles[i].transform.right;
            }
            LaserAudio.Play();
            yield return new WaitForSeconds(0.2f / shootDelay);
        }
        Shooting = true;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
