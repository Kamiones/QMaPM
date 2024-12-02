using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public GameObject PopUpPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPopUp()
    {
        Debug.Log("Intentando abrir PopUp...");

        if (PopUpPrefab != null)
        {
            Debug.Log("PopUpPrefab es no nulo, ahora activando...");
            PopUpPrefab.SetActive(true);
        }
        else
        {
            Debug.LogError("PopUpPrefab no está asignado.");
        }
    }

    public void ClosePopUp()
    {
        Debug.Log("Intentando cerrar PopUp...");
        PopUpPrefab.SetActive(false);
    }



}
