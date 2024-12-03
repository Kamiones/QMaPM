using UnityEngine;

[CreateAssetMenu(fileName = "PistaObjetoSO", menuName = "Scriptables/Pistas/Objeto")]
public class PistaObjeto : Pista
{
    [SerializeField] private GameObject asset;

    public override void CrearPista()
    {
        Item i = SpawnearPista(GameManager.Instance.objetoPrefab);
        if(asset!=null) Instantiate(asset, pos, Quaternion.identity).transform.SetParent(i.transform);
    }

}