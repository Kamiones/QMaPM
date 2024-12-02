using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    [SerializeField] private Nivel[] niveles = new Nivel[1];
    public GameObject objetoPrefab, npcPrefab;

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
            for (int j = 0; j < i; j++)
            {
                if (array[i] == array[j])
                {
                    array[i] = null;
                    Debug.LogError($"No se permiten {elem} duplicad{(o?"o":"a")}s");
                    break;
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
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        GenerateLevel(0);
    }

    private void GenerateLevel(int n)
    {
        Nivel currentNivel = niveles[n];
        Sospechoso[] sospechosos = (Sospechoso[])currentNivel.sospechosos.Clone();
        int r = Random.Range(0, sospechosos.Length);
        (sospechosos[0], sospechosos[r]) = (sospechosos[r], sospechosos[0]);
        int[] pistas_Sospechosos = new int[sospechosos.Length];
        pistas_Sospechosos[0] = currentNivel.CalcularNPistasCorrectas();
        int pistasRestantes = currentNivel.nPistas - pistas_Sospechosos[0];
        for (int i = 0; i < currentNivel.nPistas; i++)
        {
            pistas_Sospechosos[i] = 0;
        }
        for (int i = 1; i < sospechosos.Length; i++)
        {
            sospechosos[i].CrearPistas(pistas_Sospechosos[i]);
        }
        StartGame();
    }

    private void StartGame()
    {

    }

}