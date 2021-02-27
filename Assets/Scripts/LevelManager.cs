using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;

    public int gemsCollected;

    public string levelToLoad;

    public float timeInLevel; 

    void Awake()
    {
        instance = this;

        timeInLevel = 0f;
    }

    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn - (1f / UI.instance.fadeSpeed));

        UI.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UI.instance.fadeSpeed) + .2f);

        UI.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.sr.flipX = false;
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        if (PlayerHealth.instance.currentHealth >= 2)
        {
            PlayerHealth.instance.currentHealth -= 2;
            UI.instance.UpdateHealthDisplay();
        } else if (PlayerHealth.instance.currentHealth <= 2)
        {
            PlayerHealth.instance.currentHealth = PlayerHealth.instance.maxHealth;
            UI.instance.UpdateHealthDisplay();
        }
        
    }

    public void EndLevel()
    {
        StartCoroutine("EndLevelCo");
    }

    public IEnumerator EndLevelCo()
    {
        AudioManager.instance.PlayLevelVictory();

        PlayerController.instance.stopInput = true;

        CameraFollow.instance.stopFollow = true;

        UI.instance.levelCompleteText.SetActive(true);

        yield return new WaitForSeconds(1f);

        UI.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UI.instance.fadeSpeed) + 3f);
        SaveData();

        SceneManager.LoadScene(levelToLoad);

        SaveData();

    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }

        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if (timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }
    }
}
