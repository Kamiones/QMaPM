using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
