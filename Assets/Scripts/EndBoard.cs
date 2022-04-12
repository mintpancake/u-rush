using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBoard : MonoBehaviour
{
    public static EndBoard instance; 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI attributeText;
    private GameObject storyButton;
    public TextMeshProUGUI storyText;
    private static int score;
    private static int[] attributes = { 0, 0, 0, 0 }; // Academy, Happiness, Wealth, Health

    public GameObject[] characterList;
    public GameObject[] stars;
    private static int starIndex;

    private static bool storyOn = false;

    private static string honor;
    private static string academy;
    private static string happiness;
    private static string wealth;
    private static string health;

    private static string storyWords;

    public float fadeInSpeed = 1f;
    public float fadeOutSpeed = 2f;
    public RawImage rawImage;
    public RectTransform rectTransform;
    private string nextSceneName;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        score = GameManager.score;
        attributes = GameManager.attributes;
        scoreText.SetText("SCORE: " + score);
        attributeText.SetText("Academy: {0}\nHappiness: {1}\nWealth: {2}\nHealth: {3}", attributes[0], attributes[1], attributes[2], attributes[3]);
        storyButton = GameObject.Find("Story");
        //storyButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
    }

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        StartCoroutine(FadeInScene());

        storyText.SetText("");

        storyOn = false;

        honor = "";
        academy = "";
        happiness = "";
        wealth = "";
        health = "";
        storyWords = "";

        if (storyButton.activeInHierarchy)
            storyButton.SetActive(false);

        StartCoroutine(ActivationRoutine());

        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if (characterList[CharacterIndex.index])
        {
            characterList[CharacterIndex.index].SetActive(true);
            characterList[CharacterIndex.index].GetComponent<Animator>().Play("Unarmed_Idle2", 0);
        }

        foreach (GameObject go in stars)
        {
            go.SetActive(false);
        }

        storyText.SetText("");

        if (attributes[0]>=attributes[1] && attributes[0] >= attributes[2] && attributes[0] >= attributes[3])
        {
            starIndex = 0;
        }
        else if (attributes[1] >= attributes[0] && attributes[1] >= attributes[2] && attributes[1] >= attributes[3])
        {
            starIndex = 1;
        }
        else if (attributes[2] >= attributes[0] && attributes[2] >= attributes[1] && attributes[2] >= attributes[3])
        {
            starIndex = 2;
        }
        else if (attributes[3] >= attributes[0] && attributes[3] >= attributes[1] && attributes[3] >= attributes[2])
        {
            starIndex = 3;
        }

        if (score>10000)
        {
            honor = "first class honor.";
        }
        else if (score>7000 && score<=10000)
        {
            honor = "generally good performance.";
        }
        else
        {
            honor = "poor performance.";
        }

        if (attributes[0] > 800)
        {
            academy += " During college, you did pretty well in your academics.";
            if (attributes[0] > 1000 && starIndex==0)
                academy += " You are highly fueled up when it comes to studying, yet you might as well want to consider exploring other things in your college life.";
        }
        else if (attributes[0]>400 && attributes[0] <= 800)
        {
            academy += " During college, you perform moderately well in your academics.";
        }
        else
        {
            academy += " During college, you might need to work harder in your academics.";
            if (attributes[0] < 200)
            {
                academy += " Try to reach out your friends to form study groups to stay motivated!";
            }
        }

        if (attributes[1] > 800)
        {
            happiness += " You are so lit up on your college life.";
            if (attributes[1] > 1000)
            {
                happiness += " It is definitely one of your life highlight.";
            }
        }
        else if (attributes[1]>400 && attributes[1] <= 800)
        {
            happiness += " You feel quite okay in college life.";
        }
        else
        {
            happiness += " College life is probably not the best part of your life.";
            if (attributes[1] < 200)
            {
                happiness += " But hey! Rainbows come up after storms. You never know what's good on your life ahead, right?";
            }
        }

        if (attributes[2] > 800)
        {
            wealth += " You've got a bunch of cash.";
            if (attributes[2] > 1000 && starIndex==2)
            {
                wealth += " Comparing to an A+ in a course, you somehow prefer a wad of banknotes?";
            }
        }
        else if (attributes[2] > 400 && attributes[2] <= 800)
        {
            wealth += " You managed to save some money.";
        }
        else
        {
            wealth += " You generally spent a lot.";
            if (attributes[2] < 200)
            {
                wealth += " Perhaps, it's time for you to start being aware on your money management!";
            }
        }

        if (attributes[3] > 800)
        {
            health += " One last thing is, you've come to manage your health pretty well.";
            if (attributes[3] > 1000)
            {
                health += " It seems like you rather treat it as one of your best life investment.";
            }
        }
        else if (attributes[3] >400 && attributes[3] <= 800)
        {
            health += " One last thing is, you've somehow maintained a relatively good health.";
        }
        else
        {
            health += " One last thing is, it seems like you've been neglecting your health most of the time.";
            if (attributes[3] < 200)
            {
                health += " Although you are young, you need to pay extra attention on your health!";
            }
        }

        storyWords = "Congratulations! You have completed your college life with " + honor + academy + happiness + wealth + health;
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
            yield return null;
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

    private IEnumerator ActivationRoutine()
    {

        yield return new WaitForSeconds(3.5f);

        storyButton.SetActive(true);
        stars[starIndex].SetActive(true);
    }

    public static void Restart()
    {
        Time.timeScale = 1f;
        ResetState();
    }

    public static void ResetState()
    {
        GameManager.deducted = 0;
        GameManager.score = 0;
        GameManager.attributes[0] = 0;
        GameManager.attributes[1] = 0;
        GameManager.attributes[2] = 0;
        GameManager.attributes[3] = 0;
        GameManager.semester = 0;
        GameManager.finish = false;
        GameManager.paused = false;
        honor = "";
        academy = "";
        happiness = "";
        wealth = "";
        health = "";
        storyWords = "";
        CharacterIndex.index = 0;
    }

    public static void Quit()
    {
        Application.Quit();
    }

    public static void StoryPress()
    {
        if (storyOn == false)
        {
            instance.stars[starIndex].SetActive(false);
            instance.scoreText.SetText("");
            instance.attributeText.SetText("");
            instance.storyText.SetText(storyWords);
            instance.storyButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "CHECK SCORE";
            storyOn = true;

        }
        else
        {
            instance.stars[starIndex].SetActive(true);
            instance.scoreText.SetText("SCORE: " + score);
            instance.attributeText.SetText("Academy: {0}\nHappiness: {1}\nWealth: {2}\nHealth: {3}", attributes[0], attributes[1], attributes[2], attributes[3]);
            instance.storyText.SetText("");
            instance.storyButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "CHECK STORY";
            storyOn = false;
        }
    }



}
