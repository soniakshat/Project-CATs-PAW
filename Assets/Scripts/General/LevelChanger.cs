using UnityEngine;
using UnityEngine.SceneManagement;

// ######################################### //
// Script to change level and goto main menu //
// ######################################### //

public class LevelChanger : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] private GameObject _PauseMenuUI;
    [SerializeField] private string _NextLevel; // Get next level 


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void NL()
    {
        SceneManager.LoadScene(_NextLevel); // load next level
    }

    public void MM()
    {
        // UnLock cursor
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
        Time.timeScale = 1f;
        isGamePaused = false;
        SceneManager.LoadScene(0); // load main menu
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        // UnLock cursor
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
        if (!isGamePaused)
        {
            _PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isGamePaused = true;
        }
    }

    public void ResumeGame()
    {
        // Lock cursor
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        if (isGamePaused)
        {
            _PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isGamePaused = false;
        }
    }

    public void ExitGame(){
        Application.Quit();
        print("Application.Quit() is called");
    }

}
