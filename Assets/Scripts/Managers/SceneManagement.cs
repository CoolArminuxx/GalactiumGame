using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private AudioSource Music;
    [SerializeField] private AudioSource MainMenuSound;
    [SerializeField] private GameObject FadeBackground;
    [SerializeField] private LivesSO LivesRef;
    [SerializeField] private GameObject Button;
    private PlayerActions controls = null;
    public float delayTime = 5f;
    public bool MainMenu;
    public bool tutorial;
    public bool end;
    public GameObject[] KeyVariants;

    private void Awake()
    {
        Cursor.visible = false;
        controls = new PlayerActions();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    public void Start()
    {
        if (Button != null)
        {
            StartCoroutine(ButtonActivation());
        }
    }

    public void Update()
    {
        if (MainMenu == true)
        {
            if (Gamepad.current != null)
            {
                KeyVariants[0].SetActive(false);
                KeyVariants[1].SetActive(false);
                KeyVariants[2].SetActive(true);
                KeyVariants[3].SetActive(true);
            }
            else if (Gamepad.current == null)
            {
                KeyVariants[0].SetActive(true);
                KeyVariants[1].SetActive(true);
                KeyVariants[2].SetActive(false);
                KeyVariants[3].SetActive(false);
            }
            if (controls.Gameplay.Play.ReadValue<float>() > 0)
            {
                MainMenuSound.Play();
                Tutorial();
                MainMenu = false;
            }
            if (controls.Gameplay.Exit.ReadValue<float>() > 0)
            {
                Debug.Log("Quit");
                Application.Quit();
                MainMenu = false;
            }
        }
        if (tutorial == true)
        {
            if (Gamepad.current != null)
            {
                KeyVariants[0].SetActive(false);
                KeyVariants[1].SetActive(true);
            }
            else if (Gamepad.current == null)
            {
                KeyVariants[0].SetActive(true);
                KeyVariants[1].SetActive(false);
            }
            if (controls.Gameplay.Play.ReadValue<float>() > 0)
            {
                StartGame();
                tutorial = false;
            }
        }
        if (end == true)
        {
            if (Gamepad.current != null)
            {
                KeyVariants[0].SetActive(false);
                KeyVariants[1].SetActive(true);
            }
            else if (Gamepad.current == null)
            {
                KeyVariants[0].SetActive(true);
                KeyVariants[1].SetActive(false);
            }
            if (controls.Gameplay.Play.ReadValue<float>() > 0)
            {
                PlayAgain();
                end = false;
            }
        }
    }

    public void Tutorial()
    {
        StartCoroutine(ToTutorial());
    }

    public void StartGame()
    {
        StartCoroutine(GameScene());
    }
    public void Restart()
    {
        StartCoroutine(DeathRestart());
    }

    public void PlayAgain()
    {
        StartCoroutine(ToMainMenu());
    }

    public IEnumerator DeathRestart()
    {
        if (LivesRef.Value <= 0)
        {
            FadeBackground.SetActive(true);
            StartCoroutine(FadeOut(Music, 3f));
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(3);
        }
        else
        {
            FadeBackground.SetActive(true);
            StartCoroutine(FadeOut(Music, 3f));
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(1);
        }
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        float startVolume = audioSource.volume;
        audioSource.volume = 0;

        while (audioSource.volume < 0.5f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        //audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator GameScene()
    {
        FadeBackground.SetActive(true);
        StartCoroutine(FadeOut(Music, 3f));
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }

    public IEnumerator EndScene()
    {
        FadeBackground.SetActive(true);
        StartCoroutine(FadeOut(Music, 3f));
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(2);
    }

    public IEnumerator ToMainMenu()
    {
        FadeBackground.SetActive(true);
        StartCoroutine(FadeOut(Music, 3f));
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }

    public IEnumerator ButtonActivation()
    {
        yield return new WaitForSeconds(3);
        Button.SetActive(true);
        yield return new WaitForSeconds(1);
        //Button.GetComponent<Button>().Select();
    }

    public IEnumerator ToTutorial()
    {
        FadeBackground.SetActive(true);
        StartCoroutine(FadeOut(Music, 3f));
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(4);
    }
}
