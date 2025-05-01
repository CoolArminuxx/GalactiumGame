using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour
{
    public Image HealthBar;
    public int MaxHealth;
    private int EnemyHealth;
    [SerializeField] private PathSystem pathRef;
    [SerializeField] private Enemy_SpawnSystem spawnRef;
    [SerializeField] private ScoreSO scoreRef;

    private void Start()
    {
        if (pathRef.isBoss == true)
        {
            HealthBar = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Image>();
        }

        EnemyHealth = MaxHealth;
        spawnRef = GameObject.Find("SpawnManager").GetComponent<Enemy_SpawnSystem>();
    }
    public void DamageEnemy()
    {
        if (EnemyHealth > 0)
        {
            EnemyHealth -= 1;
            HealthBar.fillAmount -= 1f / MaxHealth;
        }
        if (EnemyHealth <= 0 && pathRef.isBoss == true)
        {
            HealthBar.enabled = false;
        }
        if (EnemyHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        GameObject spawnedExplosion = Pool.Instance.GetExplosion();
        if (spawnedExplosion != null)
        {
            spawnedExplosion.transform.position = transform.position;
            spawnedExplosion.SetActive(true);
            spawnedExplosion.GetComponent<AudioSource>().Play();
        }
        scoreRef.Score += 200;
        scoreRef.Kills++;
        spawnRef.CurrentEnemyCount--;
        pathRef.EndPathFollow();
        EnemyHealth = MaxHealth;
        HealthBar.fillAmount = 1;
        gameObject.SetActive(false);
    }
}
