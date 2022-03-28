using UnityEngine;
using UnityEngine.SceneManagement;


public class GoMainMenu : MonoBehaviour
{
    // Lataa Main Menu Scripti
    void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
