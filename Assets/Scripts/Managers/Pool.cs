using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    public List<GameObject> BasicEnemyShipPool = new List<GameObject>();
    private int BasicEnemyShipPoolAmount = 5;
    [SerializeField] private GameObject BasicEnemyShipPrefab;

    private List<GameObject> SprinterEnemyShipPool = new List<GameObject>();
    private int SprinterEnemyShipPoolAmount = 5;
    [SerializeField] private GameObject SprinterEnemyShipPrefab;

    private List<GameObject> TankEnemyShipPool = new List<GameObject>();
    private int TankEnemyShipPoolAmount = 5;
    [SerializeField] private GameObject TankEnemyShipPrefab;

    private List<GameObject> EnemyBulletPool = new List<GameObject>();
    private int EnemyBulletPoolAmount = 30;
    [SerializeField] private GameObject EnemyBulletPrefab;

    public List<GameObject> PlayerBulletPool = new List<GameObject>();
    private int PlayerBulletPoolAmount = 30;
    [SerializeField] private GameObject PlayerBulletPrefab;

    private List<GameObject> ExplosionPool = new List<GameObject>();
    private int ExplosionPoolAmount = 10;
    [SerializeField] private GameObject ExplosionPrefab;

    private List<GameObject> MuzzleFlashPool = new List<GameObject>();
    private int MuzzleFlashPoolAmount = 60;
    [SerializeField] private GameObject MuzzleFlashPrefab;

    private List<GameObject> HitFlashPool = new List<GameObject>();
    private int HitFlashPoolAmount = 60;
    [SerializeField] private GameObject HitFlashPrefab;

    private void Start()
    {
        FillPool(BasicEnemyShipPool, BasicEnemyShipPrefab, BasicEnemyShipPoolAmount);
        FillPool(SprinterEnemyShipPool, SprinterEnemyShipPrefab, SprinterEnemyShipPoolAmount);
        FillPool(TankEnemyShipPool, TankEnemyShipPrefab, TankEnemyShipPoolAmount);
        FillPool(EnemyBulletPool, EnemyBulletPrefab, EnemyBulletPoolAmount);
        FillPool(PlayerBulletPool, PlayerBulletPrefab, PlayerBulletPoolAmount);
        FillPool(ExplosionPool, ExplosionPrefab, ExplosionPoolAmount);
        FillPool(MuzzleFlashPool, MuzzleFlashPrefab, MuzzleFlashPoolAmount);
        FillPool(HitFlashPool, HitFlashPrefab, HitFlashPoolAmount);
    }

    private void FillPool(List<GameObject> poolToFill, GameObject Prefab, int numberInPool)
    {
        for(int i = 0; i < numberInPool; i++)
        {
            GameObject itemToPool = Instantiate(Prefab);
            itemToPool.transform.parent = transform;
            itemToPool.SetActive(false);
            poolToFill.Add(itemToPool);
        }
    }

    private GameObject GetPooledObject(List<GameObject> listToLookAt)
    {
        for (int i = 0; i < listToLookAt.Count; i++)
        {
            if (!listToLookAt[i].activeInHierarchy)
            {
                return listToLookAt[i];
            }
        }

        return null;
    }

    public GameObject GetBasicEnemyShip()
    {
        return GetPooledObject(BasicEnemyShipPool);
    }

    public GameObject GetSprinterEnemyShip()
    {
        return GetPooledObject(SprinterEnemyShipPool);
    }

    public GameObject GetTankEnemyShip()
    {
        return GetPooledObject(TankEnemyShipPool);
    }

    public GameObject GetEnemyBullet()
    {
        return GetPooledObject(EnemyBulletPool);
    }

    public GameObject GetPlayerBullet()
    {
        return GetPooledObject(PlayerBulletPool);
    }

    public GameObject GetExplosion()
    {
        return GetPooledObject(ExplosionPool);
    }

    public GameObject GetMuzzleFlash()
    {
        return GetPooledObject(MuzzleFlashPool);
    }

    public GameObject GetHitFlash()
    {
        return GetPooledObject(HitFlashPool);
    }
}
