using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string startScene, continueScene;

    public GameObject continueButton;

    private void Start()
    {
        if (PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);

        PlayerPrefs.DeleteAll();

        
    }

    public void ContinueGame()
    {
        PlayerPrefs.GetString("CurrentLevel", SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(continueScene);

        Time.timeScale = 1;
        
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
