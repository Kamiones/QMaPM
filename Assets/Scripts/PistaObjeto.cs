using UnityEngine;

[CreateAssetMenu(fileName = "PistaObjetoSO", menuName = "Scriptables/Pistas/Objeto")]
public class PistaObjeto : Pista
{
    [SerializeField] private GameObject asset;

    public override void CrearPista()
    {
        Item i = SpawnearPista(GameManager.Instance.objetoPrefab);
        if (asset != null)
        {
            i.transform.GetChild(0).gameObject.SetActive(false);
            Transform aux = Instantiate(asset, Vector3.zero, Quaternion.identity).transform;
            aux.SetParent(i.transform);
            aux.localPosition = Vector3.zero;
        }
    }

}