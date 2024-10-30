using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaManager : MonoBehaviour
{
    public MeshRenderer pantallaRenderer; // El MeshRenderer de la pantalla
    public Material[] imagenesMateriales; // Array de materiales
    private int currentIndex = 0;

    // Muestra el siguiente material en el array
    public void NextDisplay()
    {
        if (imagenesMateriales != null && imagenesMateriales.Length > 0)
        {
            currentIndex = (currentIndex + 1) % imagenesMateriales.Length;
            pantallaRenderer.material = imagenesMateriales[currentIndex];
            Debug.Log("Material actualizado a índice: " + currentIndex);
        }
    }

    // Muestra un material específico basado en el índice pasado
    public void DisplaySpecificImage(int index)
    {
        if (imagenesMateriales != null && index >= 0 && index < imagenesMateriales.Length)
        {
            pantallaRenderer.material = imagenesMateriales[index];
            Debug.Log("Mostrando imagen específica: índice " + index);
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango en DisplaySpecificImage.");
        }
    }
}

/* para implementar en los scripts:

public class Sarten : MonoBehaviour
{
    public PantallaManager pantalla;

    private void EncenderSarten()
    {
        // Lógica para encender el sartén
        pantalla.NextDisplay();
    }
}

*/