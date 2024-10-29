using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaManager : MonoBehaviour
{
    public MeshRenderer pantallaRenderer; // El MeshRenderer de la pantalla
    public Material[] imagenesMateriales; // Array de materiales
    private int currentIndex = 0;

    public void NextDisplay()
    {
        if (imagenesMateriales != null && imagenesMateriales.Length > 0)
        {
            currentIndex = (currentIndex + 1) % imagenesMateriales.Length; // Avanza al siguiente material
            pantallaRenderer.material = imagenesMateriales[currentIndex];
            Debug.Log("Material actualizado a índice: " + currentIndex);
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