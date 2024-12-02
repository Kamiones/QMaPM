using UnityEngine;

[CreateAssetMenu(fileName = "PistaNPCSO", menuName = "Scriptables/Pistas/NPC")]
public class PistaNPC : Pista
{
    [TextArea] public string dialogo;

    public override void CrearPista()
    {
        var obj = Instantiate(GameManager.Instance.npcPrefab, pos, Quaternion.identity);
        obj.name = dialogo;
    }

}