using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)                
                ResumeGame();
            else
                PauseGame();
        }   
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenuUI.SetActive(true);        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenuUI.SetActive(false);        
    }
}

