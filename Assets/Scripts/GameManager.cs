using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    [SerializeField] private Nivel[] niveles = new Nivel[1];
    private Nivel currentNivel;
    private Sospechoso culpable;
    [Header("Assets")]
    public Susss sospechosoPrefab;
    public Item objetoPrefab, npcPrefab;
    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] TimerState[] timerStates;
    private int currentState;
    private bool isLastState, pingPong;
    private Vector3[] suspectsRandomPos;

    [Serializable]
    public class TimerState
    {
        public Vector2Int remainingTime;
        public AudioClip newMusic;
        public Color newColor;
        public bool pingPong;
    }

    #region UnityEditor
#if UNITY_EDITOR
    public static void CheckMinArraySize<T>(ref T[] array, int min, string elem) where T : ScriptableObject
    {
        if (array.Length < min)
        {
            Array.Resize(ref array, min);
            Debug.LogError($"No se permite{(min>1?"n":"")} menos de {min} {elem}");
        }
    }

    public static void RemoveDuplicatedElements<T>(ref T[] array, string elem, bool o=true) where T : ScriptableObject
    {
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] != null)
            {
                for (int j = 0; j < i; j++)
                {
                    if (array[i] == array[j])
                    {
                        array[i] = null;
                        Debug.LogError($"No se permiten {elem} duplicad{(o ? "o" : "a")}s");
                        break;
                    }
                }
            }
        }
    }

    public static void ArrayHasNulls<T>(ref T[] array, string nombre)
    {
        foreach (T item in array)
        {
            if (item == null)
            {
                Debug.LogError($"{nombre} tiene nulls");
                EditorApplication.ExitPlaymode();
            }
        }
    }

    void OnValidate()
    {
        CheckMinArraySize(ref niveles, 1, "nivel");
        RemoveDuplicatedElements(ref niveles, "niveles");
    }
#endif
    #endregion

    void Awake()
    {
#if UNITY_EDITOR
        ArrayHasNulls(ref niveles, "Niveles");
#endif
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1;
        LoadLevel(0);
        StartGame();

        void StartGame()
        {
            StartCoroutine(SetTemporizador(currentNivel.tiempoTotal.x, currentNivel.tiempoTotal.y));
            //playerManager.EnableInput(true); //pendiente
        }
    }

    private void SetTimeState(int index)
    {
        TimerState current = timerStates[index];
        if (current.newMusic != null) SoundManager.Instance.PlayMusic(current.newMusic);
        if (current.newColor != new Color()) timerText.color = current.newColor;
        pingPong = current.pingPong;
    }

    private IEnumerator SetTemporizador(int min, int sec)
    {
        int mil = 0;
        SetTimerText();
        timerText.gameObject.SetActive(true);
        SetTimeState(0);
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            if (mil == 0)
            {
                mil = 999;
                if (sec == 0)
                {
                    if (min > 0)
                    {
                        sec = 59;
                        min--;
                    }
                    else break;
                }
                else sec--;
            }
            else
            {
                mil--;
                if (mil == 0) CheckTimerStates();
            }
            SetTimerText();
        }
        //Time's up

        void SetTimerText()
        {
            timerText.text = $"{min:00}:{sec:00}:{mil:000}";
        }

        void CheckTimerStates()
        {
            if (!isLastState && TimeLeft())
            {
                currentState++;
                SetTimeState(currentState);
                if (currentState >= timerStates.Length - 1) isLastState = true;
            }

            bool TimeLeft()
            {
                return 0 <= timerStates[currentState + 1].remainingTime.x;
            }
        }
    }

    void Update()
    {
        //if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R)) RestartScene();

        //backgroundMusic?.pitch = 1.5f; // Cambia el pitch de la música
        if (pingPong)
        {
            Color aux = timerText.color;
            float alpha = Mathf.PingPong(Time.time * 3f, 1f);
            timerText.color = new Color(aux.r, aux.g, aux.b, alpha);
        }
    }

    public void LoadLevel(int n)
    {
        currentNivel = niveles[n];
#if UNITY_EDITOR
        ArrayHasNulls(ref currentNivel.sospechosos, "Sospechosos");
#endif
        Sospechoso[] sospechosos = (Sospechoso[])currentNivel.sospechosos.Clone();
        int r = Random.Range(0, sospechosos.Length);
        (sospechosos[0], sospechosos[r]) = (sospechosos[r], sospechosos[0]);
        int[] pistas_Sospechosos = new int[sospechosos.Length];
        pistas_Sospechosos[0] = currentNivel.CalcularNPistasCorrectas();
        int aux = Mathf.FloorToInt((currentNivel.nPistas - pistas_Sospechosos[0]) / (sospechosos.Length - 1)); //corregir
        for (int i = 1; i < pistas_Sospechosos.Length; i++)
        {
            pistas_Sospechosos[i] = aux;
        }
        for (int i = 0; i < sospechosos.Length; i++)
        {
            Instantiate(sospechosoPrefab, suspectsRandomPos[i], Quaternion.identity).sus = sospechosos[i];
            sospechosos[i].CrearPistas(pistas_Sospechosos[i]);
        }
        culpable = sospechosos[0];
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*public void Arrest(Sospechoso sus)
    {
        EndGame(sus == culpable);
        //ShowMessage("¡Has atrapado al culpable!");
        //ShowMessage("¡Has arrestado a un inocente!");
    }

    public void GameOver()
    {
        Debug.Log("Timer finished! Game Over!");
    }

    public void EndGame(bool hasWon)
    {
        resultsText.text = hasWon? "ATRAPASTE AL CULPABLE" : "Perdiste...";
        resultsText.transform.parent.gameObject.SetActive(true);
        Time.timeScale = 0;
    }*/

}