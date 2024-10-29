using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public PantallaManager pantallaManager; // Referencia al PantallaManager
    public float tiempoEntrePasos = 3.0f; // Tiempo en segundos entre cada paso
    
    private void Start()
    {
        // Inicia la secuencia de actualización automática
        StartCoroutine(ActualizarPantallaAutomatica());
    }

    private IEnumerator ActualizarPantallaAutomatica()
    {
        while (true) // Bucle infinito, se detendrá cuando llegue al último paso
        {
            pantallaManager.NextDisplay(); // Cambia al siguiente material
            yield return new WaitForSeconds(tiempoEntrePasos); // Espera antes de cambiar de nuevo
        }
    }
}
