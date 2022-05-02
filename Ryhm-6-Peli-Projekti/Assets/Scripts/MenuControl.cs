using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuControl : MonoBehaviour
{    
    #region Main Menu Control
    // Main menu hallinta scripti

    // Peli Kentat ja menu hallinta.
    public void StartGame()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click/Click", GetComponent<Transform>().position);
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f; // Unpauses
        GameManager.manager.lives = GameManager.manager.maxLives;
    }
    public void HowToPlay()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click/Click", GetComponent<Transform>().position);
        SceneManager.LoadScene("HowToPlay");
    }
    public void BackMainMenu()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click/Click", GetComponent<Transform>().position);
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadCredits()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click/Click", GetComponent<Transform>().position);
        SceneManager.LoadScene("Credits");
    }
    public void ExitApplication() // Quit Button
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click/Click", GetComponent<Transform>().position);
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

    #endregion
}