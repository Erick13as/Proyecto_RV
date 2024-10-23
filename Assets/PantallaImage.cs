using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaManager : MonoBehaviour
{
    public MeshRenderer pantallaRenderer; // El MeshRenderer de la pantalla
    public Material[] imagenesMateriales; // Array de materiales
    private int currentIndex = 0;
    
    // Cambiar al siguiente material
    public void NextDisplay()
    {
        if (currentIndex < imagenesMateriales.Length - 1)
        {
            currentIndex++;
            pantallaRenderer.material = imagenesMateriales[currentIndex];
            Debug.Log("Cambiando a material: " + currentIndex); // Para verificar en consola
        }
    }
    
    // Retroceder al material anterior (opcional)
    public void PreviousDisplay()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            pantallaRenderer.material = imagenesMateriales[currentIndex];
        }
    }

    public void TestNextDisplay()
    {
        NextDisplay();
    }

    public void TestPreviousDisplay()
    {
        PreviousDisplay();
    }
}
