using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public static UI instance;

    public Text gemText;

    public Image heart1, heart2, heart3;

    public Sprite heartFull, heartEmpty, heartHalf;

    public Image gemIcon;

    public Animator anim;
    public Animator volumeAnim;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelCompleteText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        volumeAnim = FindObjectOfType<Animator>();

        UpdateGemCount();
        FadeFromBlack();
    }

    void Update()
    {
        if (anim == null) return;
        if (gemIcon == null) return;

        if (shouldFadeToBlack)
        {
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
                fadeScreen.gameObject.SetActive(false);
            }
        }

        else if (shouldFadeFromBlack)
        {
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
                fadeScreen.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateHealthDisplay()
    {
        switch (PlayerHealth.instance.currentHealth)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }

    public void UpdateGemCount()
    {
        gemText.text = LevelManager.instance.gemsCollected.ToString();
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

    public void ControlAudio()
    {
        AudioManager.instance.Mute();
    }

    public void MuteIcon()
    {
        if (volumeAnim.GetBool("isMuted") == false)
        {
            volumeAnim.SetBool("isMuted", true);
        }

        else if (volumeAnim.GetBool("isMuted") == true)
        {
            volumeAnim.SetBool("isMuted", false);
        }
    }
}
