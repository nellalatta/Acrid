using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool gamePaused = false;

    [SerializeField] private GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            } else {
                Pause();
            }
        }
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }


    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Can use for slow motion effects later
        gamePaused = true;
    }
}
