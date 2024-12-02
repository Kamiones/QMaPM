using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timerTime = 0f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Color warningColor = Color.red;
    [SerializeField] private AudioSource backgroundMusic;

    private bool isWarningTriggered = false;

    private void Update()
    {
        timerTime = Mathf.Max(0, timerTime - Time.deltaTime);

        int minutes = Mathf.FloorToInt(timerTime / 60f);
        int seconds = Mathf.FloorToInt(timerTime % 60f);
        int cents = Mathf.FloorToInt((timerTime % 1f) * 100f);

        timerText.text = $"{minutes:00}:{seconds:00}:{cents:000}";

        // Verifica que el pop-up se abra solo una vez cuando el tiempo llega a 10 segundos
        if (timerTime <= 10f && !isWarningTriggered)
        {
            timerText.color = warningColor;
            isWarningTriggered = true;

            if (backgroundMusic != null)
            {
                backgroundMusic.pitch = 1.5f; // Cambia el pitch de la música
            }
        }

        // Hacer el texto parpadear a medida que se acerque a 5 segundos
        if (timerTime <= 5f)
        {
            float alpha = Mathf.PingPong(Time.time * 3f, 1f);
            timerText.color = new Color(warningColor.r, warningColor.g, warningColor.b, alpha);
        }

        // Verifica si el temporizador ha llegado a 0
        if (timerTime <= 0)
        {
            OnTimerFinished();
        }
    }

    private void OnTimerFinished()
    {
        Debug.Log("Timer finished! Game Over!");

        if (backgroundMusic != null)
        {
            backgroundMusic.pitch = 1f; // Restablece el pitch de la música
        }

        Destroy(this); // Destruye el componente del temporizador
    }

    public void SetTimerTime(float newTime)
    {
        timerTime = newTime;
    }


}
