using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public string levelSelect, mainMenu;

    public GameObject pauseScreen, pauseIcon;
    public bool isPaused;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (pauseScreen == null) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseControl();
        }
    }

    public void PauseControl()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseScreen.SetActive(false);
            pauseIcon.SetActive(true);


        }
            
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseScreen.SetActive(true);
            pauseIcon.SetActive(false);
        }
            
    }

    public void LevelSelect()
    {
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(levelSelect);

        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        //GameMenuController.instance.gameObject.SetActive(true);

        Time.timeScale = 1;
    }
}
