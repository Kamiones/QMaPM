using System;
using UnityEditor.VersionControl;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Pista : ScriptableObject
{
    [SerializeField] protected Vector3 pos;
    [Header("Inventario")]
    public string nombre;
    public Sprite sprite;
    [TextArea] public string description;
    public abstract void CrearPista();

    protected void SpawnearPista(Item item)
    {
        Item i = Instantiate(item, pos, Quaternion.identity);
        i.pista = this;
    }
}

[CreateAssetMenu(fileName = "SospechosoSO", menuName = "Scriptables/SospechosoSO")]
public class Sospechoso : ScriptableObject
{
    public string nombre;
    [SerializeField] private Pista[] pistas = new Pista[3];

#if UNITY_EDITOR
    void OnValidate()
    {
        GameManager.CheckMinArraySize(ref pistas, 3, "pistas");
        GameManager.RemoveDuplicatedElements(ref pistas, "pistas", false);
    }
#endif

    public void CrearPistas(int n)
    {
        Pista[] res = ElegirPistas();
        foreach (Pista p in res)
        {
            p.CrearPista();
        }

        Pista[] ElegirPistas()
        {
            Pista[] aux = (Pista[])pistas.Clone();
            for (int i = aux.Length - 1; i > 0; i--)
            {
                int r = Random.Range(0, i + 1);
                (aux[i], aux[r]) = (aux[r], aux[i]);
            }
            Pista[] res = new Pista[n];
            Array.Copy(aux, res, n);
            return res;
        }
    }

}