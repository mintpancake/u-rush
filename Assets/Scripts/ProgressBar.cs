using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private static Image progressBar;

    public void Awake()
    {
        progressBar = transform.GetComponent<Image>();
        progressBar.type = Image.Type.Filled;
        progressBar.fillMethod = Image.FillMethod.Horizontal;
        progressBar.fillOrigin = 0;
    }
    public void Start()
    {
        SetProgressValue(0f);
    }

    public static void SetProgressValue(float value)
    {
        if (value > 1f)
        {
            value = 1f;
        }
        if (progressBar)
        {
            progressBar.fillAmount = value;
        }
    }
}
