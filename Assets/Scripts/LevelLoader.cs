using UnityEngine;
using Random = UnityEngine.Random;

public class LevelLoader : MonoBehaviour
{
    private Vector3[] suspectsRandomPos;

    void Start()
    {
        suspectsRandomPos = new Vector3[transform.childCount];
        for (int i = 0; i < suspectsRandomPos.Length; i++)
        {
            suspectsRandomPos[i] = transform.GetChild(i).position;
        }
    }

    public Nivel LoadLevel(Nivel[] niveles, int n, out Sospechoso culpable)
    {
        Nivel currentNivel = niveles[n];
#if UNITY_EDITOR
        GameManager.ArrayHasNulls(ref currentNivel.sospechosos, "Sospechosos");
#endif
        Sospechoso[] sospechosos = (Sospechoso[])currentNivel.sospechosos.Clone();
        int r = Random.Range(0, sospechosos.Length);
        (sospechosos[0], sospechosos[r]) = (sospechosos[r], sospechosos[0]);
        int[] pistas_Sospechosos = new int[sospechosos.Length];
        pistas_Sospechosos[0] = currentNivel.CalcularNPistasCorrectas();
        int aux = Mathf.FloorToInt((currentNivel.nPistas - pistas_Sospechosos[0])/(sospechosos.Length-1));
        for (int i = 1; i < pistas_Sospechosos.Length; i++)
        {
            pistas_Sospechosos[i] = aux;
        }
        for (int i = 0; i < sospechosos.Length; i++)
        {
            Instantiate(GameManager.Instance.sospechosoPrefab, suspectsRandomPos[i], Quaternion.identity);
            sospechosos[i].CrearPistas(pistas_Sospechosos[i]);
        }
        culpable = sospechosos[0];
        return currentNivel;
    }

    /*private Vector3 GetRandomPos(int i)
    {
        return suspectsRandomPos[Random.Range(0, suspectsRandomPos.Length)];
    }*/

}