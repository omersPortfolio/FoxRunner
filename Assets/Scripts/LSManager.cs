using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LSManager : MonoBehaviour
{
    public LSPlayer player;

    private MapPoint[] allPoints;

    void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>();

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach (MapPoint point in allPoints)
            {
                if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    player.transform.position = point.transform.position;
                    player.currentPoint = point;
                }
            }
        }
    }

    
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    public IEnumerator LoadLevelCo()
    {
        AudioManager.instance.PlaySFX(4);

        LSUI.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / LSUI.instance.fadeSpeed) + .25f);

        SceneManager.LoadScene(player.currentPoint.levelToLoad);
    }
}
