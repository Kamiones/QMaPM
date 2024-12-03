
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtener el script ArrestNPC del jugador y aumentar el conteo de pistas
            //other.GetComponent<ArrestNPC>().cluesFound++;
            // Desactivar o destruir la pista
            gameObject.SetActive(false);
        }
    }
}