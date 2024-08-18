using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused = false;
    public MoveController moveController;
    private void Start()
    {
        AudioManager.instance.Play("Music");
    }
    void Update()
    {
        // Detectar si se presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
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
        pauseMenuUI.SetActive(false); // Ocultar el menú de pausa
        Time.timeScale = moveController.TimeActual;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); // Mostrar el menú de pausa
        Time.timeScale = 0f; // Pausar el juego
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        SceneManager.LoadScene("MainMenu"); // Cargar la escena del menú principal
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit(); // Salir del juego
    }
}
