using System;
using UnityEngine;
using TMPro;

[Serializable]
public class TimerState
{
    public int remainingTime;
    public AudioClip newMusic;
    public Color newColor;
    public bool pingPong;
}

public class Timer : MonoBehaviour
{
    private float remainingTime = 0f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] TimerState[] timerStates;
    private int currentState = 0;
    private bool pingPong = false;

    void Update()
    {
        if (remainingTime <= 0) OnTimerFinished();
        else
        {
            //remainingTime = Mathf.Max(0, remainingTime - Time.deltaTime);
            remainingTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            int cents = Mathf.FloorToInt((remainingTime % 1f) * 100f);

            timerText.text = $"{minutes:00}:{seconds:00}:{cents:000}";

            //backgroundMusic?.pitch = 1.5f; // Cambia el pitch de la música
            if (pingPong)
            {
                Color aux = timerText.color;
                float alpha = Mathf.PingPong(Time.time * 3f, 1f);
                timerText.color = new Color(aux.r, aux.g, aux.b, alpha);
            }

            CheckTimeStates();
        }

        void CheckTimeStates()
        {
            if(remainingTime <= timerStates[currentState+1].remainingTime)
            {
                currentState++;
                SetTimeState(currentState);
            }
        }

    }

    public void SetTimerTime(float newTime)
    {
        remainingTime = newTime;
        gameObject.SetActive(true);
        SetTimeState(currentState);
    }

    private void SetTimeState(int index)
    {
        TimerState current = timerStates[index];
        if(current.newMusic != null) GameManager.Instance.soundManager.PlayMusic(current.newMusic);
        if(current.newColor != null) timerText.color = current.newColor;
        Debug.Log(current.newColor);
        pingPong = current.pingPong;
    }

    private void OnTimerFinished()
    {
        GameManager.Instance.GameOver();
        //backgroundMusic?.pitch = 1f; // Restablece el pitch de la música
        Destroy(this); // Destruye el componente del temporizador
    }

}