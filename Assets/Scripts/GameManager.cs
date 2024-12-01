using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Agregamos para poder recargar la escena

public class GameManager : MonoBehaviour
{
    public GameObject victoryScreen; // Pantalla de victoria
    public GameObject loseScreen;    // Pantalla de derrota

    void Start()
    {
        // Ocultar las pantallas al inicio
        victoryScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    void Update()
    {
        // Si alguna pantalla final está activa y se presiona R
        if ((victoryScreen.activeSelf || loseScreen.activeSelf) && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // Método para finalizar el juego
    public void EndGame(bool hasWon)
    {
        if (hasWon)
        {
            victoryScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }

        // Detener el tiempo del juego
        Time.timeScale = 0;
    }

    // Método para reiniciar el juego
    private void RestartGame()
    {
        // Restaurar el tiempo
        Time.timeScale = 1;
        // Ocultar pantallas
        victoryScreen.SetActive(false);
        loseScreen.SetActive(false);
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
