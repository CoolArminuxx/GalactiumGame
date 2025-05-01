using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Linq;
using UnityEngine.InputSystem;

public class TerminalManager : MonoBehaviour
{
    [SerializeField] private ScoreSO ScoreRef;
    [SerializeField] private LivesSO LivesRef;
    [SerializeField] private Player_Degradation DegRef;
    [SerializeField] private Player_Health HealthRef;
    [SerializeField] private WaveDisplay WaveRef;
    [SerializeField] private SceneManagement SceneRef;
    [SerializeField] private Enemy_SpawnSystem SpawnRef;
    [SerializeField] private Player_Movement MovementRef;
    [SerializeField] private Player_Gun GunRef;
    [SerializeField] private Pool poolRef;

    [SerializeField] private GameObject Terminal;
    [SerializeField] private GameObject[] Upgrades;
    [SerializeField] private GameObject[] UpgradeSlots;
    [SerializeField] private Image[] ShipParts;
    [SerializeField] private TMP_Text[] ShipPercentages;
    [SerializeField] private Animator TerminalAnimator;
    [SerializeField] private AudioSource MainMusic;
    [SerializeField] private AudioSource TerminalMusic;

    private bool Activate = true;
    public List<int> randomNumbers = new List<int>();
    public List<GameObject> Guns = new List<GameObject>();
    public List<GameObject> Powerups = new List<GameObject>();
    private int gunNum = 0;
    private int randomNum;
    public int ButtonSelected;
    private int totalKills;
    public bool CanSelect;

    public GameObject[] KeyVariants;
    public bool powerupShown;

    private void Update()
    {
        CheckAllEnemiesKilled();

        if (Gamepad.current != null)
        {
            if (powerupShown == true)
            {
                KeyVariants[0].SetActive(false);
                KeyVariants[1].SetActive(true);
            }
            else
            {
                KeyVariants[0].SetActive(false);
                KeyVariants[1].SetActive(false);
            }
        }
        else if (Gamepad.current == null)
        {
            if (powerupShown == true)
            {
                KeyVariants[0].SetActive(true);
                KeyVariants[1].SetActive(false);
            }
            else
            {
                KeyVariants[0].SetActive(false);
                KeyVariants[1].SetActive(false);
            }
        }
    }

    private void CheckAllEnemiesKilled()
    {
        if (ScoreRef.Kills == SpawnRef.EnemiesInWave + totalKills && Activate == true && SpawnRef.BossSpawned == false)
        {
            totalKills = ScoreRef.Kills;
            SetShipHealth();
            SetUpgrades();
            StartCoroutine(TerminalActivation());
            WaveRef.StartWaveMove();
            Activate = false;
        }

        if (ScoreRef.Kills == SpawnRef.EnemiesInWave + totalKills && Activate == true && SpawnRef.BossSpawned == true)
        {
            SceneRef.StartCoroutine(SceneRef.EndScene());
        }
    }

    private void SetUpgrades()
    {
        if (randomNumbers.Count < 3)
        {
            RandomNumberSelection();
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Upgrades[randomNumbers[i]].SetActive(true);
                Upgrades[randomNumbers[i]].GetComponent<RectTransform>().anchoredPosition = UpgradeSlots[i].GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }

    public void UpgradeSelection()
    {
        int UpgradeActivate = randomNumbers[ButtonSelected];
        switch (UpgradeActivate)
        {
            case 0:
                LivesRef.Value++;
                HealthRef.Lives[LivesRef.Value - 1].SetActive(true);
                StartCoroutine(CloseTerminal());
                Debug.Log("common1");
                break;
            case 1:
                MovementRef.maxSpeed += 2;
                MovementRef.SetSpeed();
                StartCoroutine(CloseTerminal());
                Debug.Log("common2");
                break;
            case 2:
                GunRef.shootDelay += 0.5f;
                StartCoroutine(CloseTerminal());
                Debug.Log("common3");
                break;
            case 3:
                for (int i = 0; i < poolRef.PlayerBulletPool.Count; i++)
                {
                    poolRef.PlayerBulletPool[i].transform.DOScaleX(poolRef.PlayerBulletPool[i].transform.localScale.x * 2, 0);
                    poolRef.PlayerBulletPool[i].transform.DOScaleY(poolRef.PlayerBulletPool[i].transform.localScale.x * 2, 0);
                }
                StartCoroutine(CloseTerminal());
                Debug.Log("common4");
                break;
            case 4:
                Guns[gunNum].SetActive(true);
                gunNum++;
                StartCoroutine(CloseTerminal());
                Debug.Log("rare1");
                break;
            case 5:
                for (int i = 0; i < Powerups.Count; i++)
                {
                    Powerups[i].SetActive(false);
                }
                powerupShown = true;
                Powerups[0].SetActive(true);
                StartCoroutine(CloseTerminal());
                Debug.Log("rare2");
                break;
            case 6:
                for (int i = 0; i < Powerups.Count; i++)
                {
                    Powerups[i].SetActive(false);
                }
                powerupShown = true;
                Powerups[1].SetActive(true);
                StartCoroutine(CloseTerminal());
                Debug.Log("rare3");
                break;
            case 7:
                for (int i = 0; i < Powerups.Count; i++)
                {
                    Powerups[i].SetActive(false);
                }
                powerupShown = true;
                Powerups[2].SetActive(true);
                StartCoroutine(CloseTerminal());
                Debug.Log("legend1");
                break;
            case 8:
                for (int i = 0; i < Powerups.Count; i++)
                {
                    Powerups[i].SetActive(false);
                }
                powerupShown = true;
                Powerups[3].SetActive(true);
                StartCoroutine(CloseTerminal());
                Debug.Log("legend2");
                break;
        }
    }

    private void RandomNumberSelection()
    {
        randomNum = Random.Range(0, Upgrades.Length);
        if (randomNumbers.Contains(randomNum))
        {
            RandomNumberSelection();
        }
        else
        {
            randomNumbers.Add(randomNum);
            SetUpgrades();
        }
    }

    private void SetShipHealth()
    {
        ShipParts[0].color = new Color(DegRef.bVal, 0, 0, 0.5f);
        ShipParts[1].color = new Color(DegRef.lVal, 0, 0, 0.5f);
        ShipParts[2].color = new Color(DegRef.rVal, 0, 0, 0.5f);
        ShipPercentages[0].text = Mathf.Round((100 - DegRef.bVal * 100) / 5) * 5 + "%";
        ShipPercentages[1].text = Mathf.Round((100 - DegRef.lVal * 100) / 5) * 5 + "%";
        ShipPercentages[2].text = Mathf.Round((100 - DegRef.rVal * 100) / 5) * 5 + "%";
    }

    public void RepairShip()
    {
        DegRef.StandardShipValues();
        SetShipHealth();
        HealthRef.SetPlayerHealth();
        StartCoroutine(CloseTerminal());
    }

    public void PowerupRepair()
    {
        DegRef.StandardShipValues();
        HealthRef.SetPlayerHealth();
    }

    private IEnumerator MusicFades()
    {
        StartCoroutine(SceneRef.FadeOut(MainMusic, 3f));
        yield return new WaitForSeconds(2f);
        StartCoroutine(SceneRef.FadeIn(TerminalMusic, 3f));
    }

    private IEnumerator MusicFadeOut()
    {
        StartCoroutine(SceneRef.FadeOut(TerminalMusic, 3f));
        yield return new WaitForSeconds(2f);
        StartCoroutine(SceneRef.FadeIn(MainMusic, 3f));
    }

    private IEnumerator TerminalActivation()
    {
        yield return new WaitForSeconds(6);
        TerminalAnimator.SetFloat("Speed", 1);
        StartCoroutine(MusicFades());
        Terminal.SetActive(true);
        yield return new WaitForSeconds(2);
        TerminalAnimator.SetFloat("Speed", 0);
        CanSelect = true;
    }

    private IEnumerator CloseTerminal()
    {
        TerminalAnimator.SetFloat("Speed", -1);
        StartCoroutine(MusicFadeOut());
        yield return new WaitForSeconds(3.5f);
        for (int i = 0; i < Upgrades.Length; i++)
        {
            Upgrades[i].SetActive(false);
        }
        randomNumbers.Clear();
        Terminal.SetActive(false);
        TerminalAnimator.SetFloat("Speed", 1);
        SpawnRef.WaveSelection();
        Activate = true;
    }
}
