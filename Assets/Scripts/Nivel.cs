using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "NivelSO", menuName = "Scriptables/NivelSO")]
public class Nivel : ScriptableObject
{
    public Vector2Int tiempoTotal = new(1,0);
    public Sospechoso[] sospechosos = new Sospechoso[3];
    [SerializeField, Min(3)] public int nPistas = 3;
    [SerializeField] private Vector2 ratioPistasCorrectas = new(0.39f, 0.95f), ratioPistasIncorrectas;

#if UNITY_EDITOR
    void OnValidate()
    {
        tiempoTotal.x = Mathf.Max(tiempoTotal.x, 1);
        GameManager.CheckMinArraySize(ref sospechosos, 3, "sospechosos");
        GameManager.RemoveDuplicatedElements(ref sospechosos, "sospechosos");
        ratioPistasCorrectas.x = Mathf.Clamp(ratioPistasCorrectas.x, 1f / sospechosos.Length + 0.05f, 0.95f);
        ratioPistasCorrectas.y = Mathf.Clamp(ratioPistasCorrectas.y, ratioPistasCorrectas.x, 0.95f);
    }
#endif

    public int CalcularNPistasCorrectas()
    {
        return Mathf.CeilToInt(nPistas * Random.Range(ratioPistasCorrectas.x, ratioPistasCorrectas.y));
    }

}