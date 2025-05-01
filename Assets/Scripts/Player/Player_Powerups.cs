using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player_Powerups : MonoBehaviour
{
    [SerializeField] private TerminalManager TerminalRef;
    [SerializeField] private GameObject PlayerShield;
    [SerializeField] private GameObject PlayerSlow;
    [SerializeField] private AudioSource SlowmoSound;
    [SerializeField] private Volume volume;
    [SerializeField] private Player_Movement MovementRef;
    [SerializeField] private Player_Gun GunRef;
    [SerializeField] private SceneManagement SceneRef;
    [SerializeField] private AudioSource MainMusic;
    [SerializeField] private AudioSource RocketmanMusic;
    private ColorAdjustments ColorGrade;
    private Bloom bloom;
    private ChromaticAberration aberration;
    private bool ReturnColorCheck;
    private float HueValue;
    private PlayerActions controls;
    public bool ShieldActive;
    public bool FreezeActive;
    public bool RocketmanActive;

    private void Awake()
    {
        controls = new PlayerActions();
    }
    private void Start()
    {
        volume.profile.TryGet(out ColorGrade);
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out aberration);
        FreezeActive = false;
        ShieldActive = false;
        RocketmanActive = false;
        aberration.active = true;
    }
    void Update()
    {
        if (controls.Gameplay.Powerup.ReadValue<float>() > 0)
        {
            PowerupCheck();
        }

        FreezeCheck();
        RocketmanCheck();
    }

    private void PowerupCheck()
    {
        TerminalRef.powerupShown = false;
        if (TerminalRef.Powerups[0].activeInHierarchy == true)
        {
            TerminalRef.Powerups[0].SetActive(false);
            TerminalRef.PowerupRepair();
        }
        if (TerminalRef.Powerups[1].activeInHierarchy == true)
        {
            TerminalRef.Powerups[1].SetActive(false);
            StartCoroutine(Shield());
        }
        if (TerminalRef.Powerups[2].activeInHierarchy == true)
        {
            TerminalRef.Powerups[2].SetActive(false);
            StartCoroutine(Freeze());
        }
        if (TerminalRef.Powerups[3].activeInHierarchy == true)
        {
            TerminalRef.Powerups[3].SetActive(false);
            StartCoroutine(Rocketman());
        }
    }

    private IEnumerator Shield()
    {
        PlayerShield.SetActive(true);
        ShieldActive = true;
        yield return new WaitForSeconds(5);
        ShieldActive = false;
        PlayerShield.SetActive(false);
    }

    private IEnumerator Freeze()
    {
        SlowmoSound.pitch = 0.8f;
        SlowmoSound.Play();
        FreezeActive = true;
        PlayerSlow.SetActive(true);
        yield return new WaitForSeconds(4);
        SlowmoSound.pitch = -0.8f;
        SlowmoSound.time = 1;
        SlowmoSound.Play();
        FreezeActive = false;
        PlayerSlow.SetActive(false);
    }

    private void FreezeCheck()
    {
        if (FreezeActive == true)
        {
            if (Time.timeScale > 0.3f)
            {
                Time.timeScale -= 0.3f * Time.deltaTime;
            }
        }
        if (FreezeActive == false)
        {
            if (Time.timeScale < 1f)
            {
                Time.timeScale += 0.3f * Time.deltaTime;
            }
        }
    }

    private IEnumerator Rocketman()
    {
        RocketmanActive = true;
        PlayerShield.SetActive(true);
        ShieldActive = true;
        MovementRef.maxSpeed += 4;
        MovementRef.SetSpeed();
        GunRef.shootDelay += 1;
        StartCoroutine(SceneRef.FadeOut(MainMusic, 1f));
        StartCoroutine(SceneRef.FadeIn(RocketmanMusic, 2f));
        yield return new WaitForSeconds(10);
        GunRef.shootDelay -= 1;
        StartCoroutine(SceneRef.FadeOut(RocketmanMusic, 1f));
        StartCoroutine(SceneRef.FadeIn(MainMusic, 2f));
        MovementRef.maxSpeed -= 4;
        MovementRef.SetSpeed();
        RocketmanActive = false;
        ShieldActive = false;
        PlayerShield.SetActive(false);
    }

    private void RocketmanCheck()
    {
        if (RocketmanActive == true)
        {
            if (ColorGrade.hueShift.value != 180 && ReturnColorCheck == false)
            {
                HueValue += 90 * Time.deltaTime;
                ColorGrade.hueShift.value = HueValue;
                bloom.intensity.value = HueValue;
            }
            if (ColorGrade.hueShift.value >= 180 && ReturnColorCheck == false)
            {
                ReturnColorCheck = true;
            }
            if (ColorGrade.hueShift.value != 0 && ReturnColorCheck == true)
            {
                HueValue -= 90 * Time.deltaTime;
                ColorGrade.hueShift.value = HueValue;
                bloom.intensity.value = HueValue;
            }
            if (ColorGrade.hueShift.value <= 0 && ReturnColorCheck == true)
            {
                ReturnColorCheck = false;
            }
        }
        else
        {
            ColorGrade.hueShift.value = 0;
            bloom.intensity.value = 20;
        }
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
