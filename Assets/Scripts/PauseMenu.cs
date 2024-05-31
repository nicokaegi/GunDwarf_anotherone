using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused)
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        UnityEngine.Debug.Log("Quit Game");  // Log message for debugging in the editor
        UnityEngine.Application.Quit();      // Quit the application

        // Optional: If you're in the editor and want to stop play mode
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}