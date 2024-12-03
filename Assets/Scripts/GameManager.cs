using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Header("Punteros")]
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private Timer timer;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private TextMeshProUGUI resultsText;
    [Header("Assets")]
    public Susss sospechosoPrefab;
    public Item objetoPrefab, npcPrefab;

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
        currentNivel = levelLoader.LoadLevel(niveles, 0, out culpable);
        StartGame();

        void StartGame()
        {
            timer.SetTimerTime(currentNivel.tiempoTotal);
            playerManager.EnableInput(true);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R)) RestartGame();

        void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Arrest(Sospechoso sus)
    {
        EndGame(sus == culpable);
        //ShowMessage("¡Has atrapado al culpable!");
        //ShowMessage("¡Has arrestado a un inocente!");
    }

    public void GameOver()
    {
        Debug.Log("Timer finished! Game Over!");
    }

    // Método para finalizar el juego
    public void EndGame(bool hasWon)
    {
        resultsText.text = hasWon? "ATRAPASTE AL CULPABLE" : "Perdiste...";
        resultsText.transform.parent.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

}