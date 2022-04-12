using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool debug = true;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI attributeText;
    public TextMeshProUGUI semesterText;
    public GameObject pausePanel;
    public GameObject pauseButton;
    public AudioSource BGM;
    public AudioClip addSound;
    public AudioClip minusSound;
    public AudioClip jumpOverSound;
    public AudioClip finishSound;

    private AudioSource soundEffect;
    private static Transform player;
    public static int deducted = 0;
    public static int score;
    public static int[] attributes = { 0, 0, 0, 0 }; // Academy, Happiness, Wealth, Health
    public static int semester = 0;
    private static int distSem = 800;
    public static bool finish = false;
    public static bool paused = false;

    public float fadeInSpeed = 1f;
    public float fadeOutSpeed = 2f;
    public RawImage rawImage;
    public RectTransform rectTransform;
    private string nextSceneName;
    private string[] sceneNames = { "RoleSelection", "MainScene", "Ending" };

    public Image hintImage;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        instance.soundEffect = GetComponent<AudioSource>();
        if (GameObject.FindWithTag("Player"))
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        if (debug)
        {
            distSem = 50;
        }
        ResetState();
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);//Ê¹±³¾°ÂúÆÁ
        StartCoroutine(FadeInScene());
        StartCoroutine(HintFade());
    }

    void Update()
    {

        if (player == null)
        {
            if (GameObject.FindWithTag("Player"))
            {
                player = GameObject.FindWithTag("Player").transform;
            }
            return;
        }

        score = (int)player.position.z + deducted;
        semester = (int)player.position.z / distSem;
        if (semester > 7)
        {
            semester = 7;
        }
        ProgressBar.SetProgressValue(player.position.z / (distSem * 8f));
        if (player.position.z > distSem * 8f)
        {
            finish = true;
        }

        scoreText.SetText("SCORE: " + score);
        attributeText.SetText("Academy: {0}\nHappiness: {1}\nWealth: {2}\nHealth: {3}", attributes[0], attributes[1], attributes[2], attributes[3]);
        semesterText.SetText("Year {0} Sem {1}", semester / 2 + 1, semester % 2 + 1);

        if (finish)
        {
            StartCoroutine(Finish());
        }
    }

    IEnumerator HintFade()
    {
        yield return new WaitForSeconds(2f);
        while (hintImage.color.a > 0.05f)
        {
            Color currColor = hintImage.color;
            currColor.a -= 0.01f;
            hintImage.color = currColor;
            yield return new WaitForSeconds(0.03f);
        }
        hintImage.color = Color.clear;
        hintImage.enabled = false;
    }

    IEnumerator FadeInScene()
    {
        while (rawImage.color.a > 0.05f)
        {
            FadeToClear();
            yield return new WaitForSeconds(0);
        }
        rawImage.color = Color.clear;
        rawImage.enabled = false;
    }

    public void FadeToScene(string sceneName)
    {
        nextSceneName = sceneName;
        if (nextSceneName != "")
        {
            rawImage.enabled = true;
            StartCoroutine(FadeoutScene());
        }
    }

    IEnumerator FadeoutScene()
    {
        while (rawImage.color.a < 0.95f)
        {
            FadeToBlack();
            yield return new WaitForSeconds(0);
        }
        if (nextSceneName == sceneNames[0] || nextSceneName == sceneNames[1])
        {
            ResetState();
        }
        SceneManager.LoadScene(nextSceneName);
    }

    private void FadeToClear()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeInSpeed * Time.deltaTime);
    }

    private void FadeToBlack()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeOutSpeed * Time.deltaTime);
    }

    IEnumerator Finish()
    {
        pauseButton.SetActive(false);
        yield return new WaitForSeconds(4f);
        FadeToScene(sceneNames[2]);
    }

    public static void Pause()
    {
        instance.pausePanel.SetActive(true);
        instance.BGM.Pause();
        Time.timeScale = 0f;
        paused = true;
    }

    public static void Resume()
    {
        instance.pausePanel.SetActive(false);
        instance.BGM.Play(0);
        Time.timeScale = 1f;
        paused = false;
    }

    public static void Restart()
    {
        Time.timeScale = 1f;
    }

    public static void Home()
    {
        Time.timeScale = 1f;
        CharacterIndex.index = 0;
    }

    public static void ResetState()
    {
        deducted = 0;
        score = 0;
        attributes[0] = 0;
        attributes[1] = 0;
        attributes[2] = 0;
        attributes[3] = 0;
        semester = 0;
        finish = false;
        paused = false;
    }

    public static void JumpOver()
    {
        if (finish || paused)
        {
            return;
        }
        deducted += Random.Range(0, 100);
        instance.soundEffect.PlayOneShot(instance.jumpOverSound, 0.5f);
    }

    public static void HitObstacle()
    {
        if (finish || paused)
        {
            return;
        }
        deducted -= Random.Range(100, 300);
        instance.soundEffect.PlayOneShot(instance.minusSound, 1f);
    }

    public static void HitItem(int type, bool add)
    {
        if (finish || paused)
        {
            return;
        }
        if (type >= 0 && type <= 3)
        {
            if (add)
            {
                attributes[type] += Random.Range(25, 75);
                //academy+ => happiness-
                if (type == 0)
                {
                    attributes[1] -= Random.Range(10, 25);
                }
                //happiness+ => academy-
                else if (type == 1)
                {
                    attributes[0] -= Random.Range(10, 25);
                }
                //wealth+ => health-
                else if (type == 2)
                {
                    attributes[3] -= Random.Range(10, 25);
                }
                //health+ => wealth-
                else
                {
                    attributes[2] -= Random.Range(10, 25);
                }
                deducted += Random.Range(0, 100);
                instance.soundEffect.PlayOneShot(instance.addSound, 0.5f);
            }
            else
            {
                attributes[type] -= Random.Range(25, 75);
                //health- => happiness+
                if (type == 3)
                {
                    attributes[1] += Random.Range(10, 25);
                }
                deducted -= Random.Range(0, 100);
                instance.soundEffect.PlayOneShot(instance.minusSound, 1f);
            }
        }
    }

    public static void HitFinishLine()
    {
        instance.soundEffect.PlayOneShot(instance.finishSound, 1f);
    }

}
