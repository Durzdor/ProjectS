using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

public class GameStateManager : MonoBehaviour
{
    // Hay cosas que no se pausan:
    // player aim tracker
    // player rotation with mouse
    // player attack vfx
    // Parecen ser todos los eventos de mouse que si se siguen actualizando
    public static bool GameIsPaused;
    
    [SerializeField] private Button pauseButton;
    
    public void InactiveState()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        pauseButton.gameObject.SetActive(false);
    }

    public void ActiveState()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Confined;
        pauseButton.gameObject.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToDesktop()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}