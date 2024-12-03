using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    [SerializeField] private Nivel[] niveles = new Nivel[1];
    private Nivel currentNivel;
    [SerializeField] private Timer timer;
    [SerializeField] private PlayerManager playerManager;
    public Item objetoPrefab, npcPrefab;
    [SerializeField] private GameObject victoryScreen, loseScreen;

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

    void OnValidate()
    {
        CheckMinArraySize(ref niveles, 1, "nivel");
        RemoveDuplicatedElements(ref niveles, "niveles");
    }
#endif

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1;
        victoryScreen.SetActive(false);
        loseScreen.SetActive(false);
        currentNivel = LevelLoader.LoadLevel(niveles, 0);
        StartGame();

        void StartGame()
        {
            timer.SetTimerTime(currentNivel.tiempoTotal);
            playerManager.EnableInput(true);
        }
    }

    void Update()
    {
        if ((victoryScreen.activeSelf || loseScreen.activeSelf) && Input.GetKeyDown(KeyCode.R)) RestartGame();

        void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GameOver()
    {
        Debug.Log("Timer finished! Game Over!");
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