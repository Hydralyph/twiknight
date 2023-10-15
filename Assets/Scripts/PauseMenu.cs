using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//kallum best 2023 270116003


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
     if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                

                AudioSource[] audios = FindObjectsOfType<AudioSource>();
                foreach (AudioSource a in audios)
                {
                    a.Play();
                }
                Resume();

            }
            else
            {
                AudioSource[] audios = FindObjectsOfType<AudioSource>();
                foreach (AudioSource a in audios)
                {
                    a.Pause();
                }
                Pause();
            }
        }   
    }
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
   void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
