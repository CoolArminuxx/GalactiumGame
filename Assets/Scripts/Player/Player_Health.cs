using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private Image HealthBar;
    [SerializeField] private LivesSO LivesRef;
    [SerializeField] private SceneManagement SceneRef;
    [SerializeField] private ScoreSO ScoreRef;
    private int PlayerHealth = 20;
    public List<GameObject> Lives = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < LivesRef.Value; i++)
        {
            Lives[i].SetActive(true);
        }
    }
    public void SetPlayerHealth()
    {
        PlayerHealth = 20;
        HealthBar.fillAmount = 1;
    }
    public void DamagePlayer()
    {
        if (PlayerHealth > 0)
        {
            PlayerHealth -= 1;
            HealthBar.fillAmount -= 0.05f;
        }
        if (PlayerHealth <= 0)
        {
            LivesRef.Value--;
            Lives[LivesRef.Value].SetActive(false);
            Death();
        }
    }

    public void Death()
    {
        GameObject spawnedExplosion = Pool.Instance.GetExplosion();
        if (spawnedExplosion != null)
        {
            spawnedExplosion.transform.position = transform.position;
            spawnedExplosion.SetActive(true);
            spawnedExplosion.GetComponent<AudioSource>().Play();
        }
        ScoreRef.Kills = 0;
        PlayerHealth = 20;
        HealthBar.fillAmount = 1f;
        SceneRef.Restart();
        gameObject.SetActive(false);
    }
}
