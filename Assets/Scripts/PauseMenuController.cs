using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Arrastra aquí tu PauseMenuPanel desde la jerarquía
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);  
        Time.timeScale = 0f;            
        isPaused = true;
      
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;            
        isPaused = false;
        
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego..."); 
        Application.Quit();            

       
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
