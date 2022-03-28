using UnityEngine;

public class MapMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject mapMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        mapMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        mapMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
