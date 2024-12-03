using UnityEngine;

[CreateAssetMenu(fileName = "PistaNPCSO", menuName = "Scriptables/Pistas/Susss")]
public class PistaNPC : Pista
{
    //public string nombre;
    [TextArea] public string dialogo;

    public override void CrearPista()
    {
        SpawnearPista(GameManager.Instance.npcPrefab);
    }

}