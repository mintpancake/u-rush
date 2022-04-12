using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    private int index = 0;
    public float fadeInSpeed = 1f;
    public float fadeOutSpeed = 2f;
    public RawImage rawImage;
    public RectTransform rectTransform;
    private string nextSceneName;


    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);//Ê¹±³¾°ÂúÆÁ
        
        StartCoroutine(FadeInScene());

        characterList = new GameObject[transform.childCount];

        // Fill the array with our models
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        // Hide the models
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if (characterList[index])
        {
            characterList[index].SetActive(true);
            characterList[index].GetComponent<Animator>().Play("Unarmed_Walk", 0);
        }
    }

    void Update()
    {

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



        public void ToggleLeft()
    {
        // Toggle off the current model
        characterList[index].SetActive(false);

        index--;
        if (index < 0)
        {
            index = characterList.Length - 1;
        }

        // Toggle on the current model
        characterList[index].SetActive(true);
        characterList[index].GetComponent<Animator>().Play("Unarmed_Walk", 0);
    }

    public void ToggleRight()
    {
        // Toggle off the current model
        characterList[index].SetActive(false);

        index++;
        if (index >= characterList.Length)
        {
            index = 0;
        }
        // Toggle on the current model
        characterList[index].SetActive(true);
        characterList[index].GetComponent<Animator>().Play("Unarmed_Walk", 0);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
