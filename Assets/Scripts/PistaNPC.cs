using UnityEngine;

[CreateAssetMenu(fileName = "PistaNPCSO", menuName = "Scriptables/Pistas/NPC")]
public class PistaNPC : Pista
{
    //public string nombre;
    [TextArea] public string dialogo;

    public override void CrearPista()
    {
        Item npc = Instantiate(GameManager.Instance.npcPrefab, pos, Quaternion.identity);
        npc.pista = this;
    }

}