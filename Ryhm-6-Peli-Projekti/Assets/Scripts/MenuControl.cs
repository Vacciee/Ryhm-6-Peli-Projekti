using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    // Main menu hallinta scripti

    // Peli Kentat ja menu hallinta.
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f; // Unpauses
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void BackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void ExitApplication() // Quit Button
    {
        Application.Quit();
    }

    // Levels
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f; // Unpauses
    }

    // Tallentaminen ja lataaminen
    public void Save()
    {
        // Tama ajetaan menusta, joka kutsuu GameManagerin savea.
        GameManager.manager.Save();
    }
    public void SaveAndQuit()
    {
        // Tama ajetaan menusta, joka kutsuu GameManagerin savea.
        GameManager.manager.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void Load()
    {
        GameManager.manager.Load();
    }
    // <---------------------------->
}