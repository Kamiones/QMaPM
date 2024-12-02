using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public PopUpManager popUpManager;

    public void OnButtonClick()
    {
        if (popUpManager != null)
        {
            popUpManager.ClosePopUp(); 
        }
    }
  
}
