using UnityEngine;

[CreateAssetMenu(fileName = "PistaObjetoSO", menuName = "Scriptables/Pistas/Objeto")]
public class PistaObjeto : Pista
{
    [SerializeField] private Sprite asset;

    public override void CrearPista()
    {
        var obj = Instantiate(GameManager.Instance.objetoPrefab, pos, Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().sprite = asset;
    }

}