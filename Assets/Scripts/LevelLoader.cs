using UnityEngine;
using Random = UnityEngine.Random;

public class LevelLoader : MonoBehaviour
{

    public static Nivel LoadLevel(Nivel[] niveles, int n)
    {
        Nivel currentNivel = niveles[n];
#if UNITY_EDITOR
        if (GameManager.ArrayHasNulls(ref currentNivel.sospechosos, "Sospechosos")) return null;
#endif
        Sospechoso[] sospechosos = (Sospechoso[])currentNivel.sospechosos.Clone();
        int r = Random.Range(0, sospechosos.Length);
        (sospechosos[0], sospechosos[r]) = (sospechosos[r], sospechosos[0]);
        int[] pistas_Sospechosos = new int[sospechosos.Length];
        pistas_Sospechosos[0] = currentNivel.CalcularNPistasCorrectas();
        int pistasRestantes = currentNivel.nPistas - pistas_Sospechosos[0];
        for (int i = 1; i < pistas_Sospechosos.Length; i++)
        {
            pistas_Sospechosos[i] = 0;
        }
        for (int i = 1; i < sospechosos.Length; i++)
        {
            Instantiate(GameManager.Instance.sospechosoPrefab, Vector3.zero, Quaternion.identity);
            sospechosos[i].CrearPistas(pistas_Sospechosos[i]);
        }
        return currentNivel;
    }

}