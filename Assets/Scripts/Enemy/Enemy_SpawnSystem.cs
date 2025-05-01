using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_SpawnSystem : MonoBehaviour
{
    [SerializeField] private Transform[] SpawnPoint;
    [SerializeField] private Transform BossSpawn;
    [SerializeField] private GameObject[] WarningIcons;
    [SerializeField] private PathSystem PathRef;
    [SerializeField] private WaveDisplay waveRef;
    [SerializeField] private SceneManagement SceneRef;
    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject BossWarningIcon;
    [SerializeField] private GameObject BossHealth;
    public int CurrentEnemyCount;
    private int TotalEnemiesKilled;
    private int MaxEnemiesInScene;
    public int EnemiesInWave;
    private int randomNum;
    private int secondsDelay;
    private int EnemyNum;
    public bool BossSpawned;

    private void Start()
    {
        secondsDelay = 4;
        WaveSelection();
    }

    public void WaveSelection()
    {
        switch (waveRef.waveNum)
        {
            case 1:
                MaxEnemiesInScene = 4;
                EnemiesInWave = 8;
                StartCoroutine(SpawnDelay());
                break;
            case 2:
                TotalEnemiesKilled = 0;
                MaxEnemiesInScene = 5;
                EnemiesInWave = 10;
                secondsDelay--;
                StartCoroutine(SpawnDelay());
                break;
            case 3:
                TotalEnemiesKilled = 0;
                MaxEnemiesInScene = 6;
                EnemiesInWave = 12;
                secondsDelay--;
                StartCoroutine(SpawnDelay());
                break;
            case 4:
                MaxEnemiesInScene = 1;
                EnemiesInWave = 1;
                StartCoroutine(BossSpawning());
                break;
            default:
                print("Error");
                break;
        }
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(secondsDelay);

        if (CurrentEnemyCount < MaxEnemiesInScene)
        {
            randomNum = Random.Range(0, 9);

            GameObject spawnedShip = null;

            switch (EnemiesInWave)
            {
                case 8:
                    EnemyNum = Random.Range(0, 1);
                    break;
                case 10:
                    EnemyNum = Random.Range(0, 2);
                    break;
                case 12:
                    EnemyNum = Random.Range(0, 3);
                    break;
            }

            switch (EnemyNum)
            {
                case 0:
                    spawnedShip = Pool.Instance.GetBasicEnemyShip();
                    break;
                case 1:
                    spawnedShip = Pool.Instance.GetSprinterEnemyShip();
                    break;
                case 2:
                    spawnedShip = Pool.Instance.GetTankEnemyShip();
                    break;
                default:
                    print("Error");
                    break;
            }

            if (spawnedShip != null)
            {
                WarningIcons[randomNum].SetActive(true);
                yield return new WaitForSeconds(2);
                WarningIcons[randomNum].SetActive(false);
                spawnedShip.transform.position = SpawnPoint[randomNum].position;
                spawnedShip.SetActive(true);
            }

            PathRef = spawnedShip.GetComponent<PathSystem>();
            PathRef.PathNum = randomNum + 5;
            PathRef.StartPath();
            CurrentEnemyCount++;
            TotalEnemiesKilled++;
        }

        if (TotalEnemiesKilled >= EnemiesInWave)
        {
            yield break;
        }

        StartCoroutine(SpawnDelay());
    }

    private IEnumerator BossSpawning()
    {
        yield return new WaitForSeconds(secondsDelay);
        BossWarningIcon.SetActive(true);
        yield return new WaitForSeconds(2);
        BossHealth.SetActive(true);
        BossWarningIcon.SetActive(false);
        Boss.transform.position = BossSpawn.position;
        Boss.SetActive(true);
        PathRef = Boss.GetComponent<PathSystem>();
        PathRef.StartPath();
        BossSpawned = true;
    }
}
