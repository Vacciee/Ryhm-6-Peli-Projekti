// using UnityEngine;

// public class MapMenu : MonoBehaviour
// {
//     #region Variables

//     public static bool GameIsPaused = false;

//     public GameObject mapMenuUI;

//     #endregion

//     void Update()
//     {
//         #region UI Inputs

//         if (Input.GetKeyDown(KeyCode.M))
//         {
//             if (GameIsPaused)
//             {
//                 Resume();
//             }
//             else
//             {
//                 Pause();
//             }
//         }

//     }
//     public void Resume()
//     {
//         mapMenuUI.SetActive(false);
//         Time.timeScale = 1f;
//         GameIsPaused = false;
//     }
//     void Pause()
//     {
//         mapMenuUI.SetActive(true);
//         Time.timeScale = 0f;
//         GameIsPaused = true;
//     }
//     #endregion
// }
