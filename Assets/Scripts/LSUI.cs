using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUI : MonoBehaviour
{

    public static LSUI instance;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelInfoPanel;
    public Text levelName, gemsFound, gemsTarget, bestTime, timeTarget;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FadeFromBlack();
    }

    
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }
        else if (shouldFadeFromBlack)
        {

            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
                fadeScreen.gameObject.SetActive(false);
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }

    public void ShowInfo(MapPoint levelInfo)
    {
        levelName.text = levelInfo.levelName;

        gemsFound.text = "FOUND: " + levelInfo.gemsCollected;
        gemsTarget.text = "IN LEVEL: " + levelInfo.totalGems;

        timeTarget.text = "TARGET: " + levelInfo.targetTime + "s";
        
        if (levelInfo.bestTime == 0)
        {
            bestTime.text = "BEST: ----";
        }else
        {
            bestTime.text = "BEST: " + levelInfo.bestTime.ToString("F2") + "s";
        }

        levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }
}
