using UnityEngine;

[CreateAssetMenu(fileName = "PistaObjetoSO", menuName = "Scriptables/Pistas/Objeto")]
public class PistaObjeto : Pista
{
    [SerializeField] private MeshRenderer asset;

    public override void CrearPista()
    {
        SpawnearPista(GameManager.Instance.objetoPrefab);
    }

}