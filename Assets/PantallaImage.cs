using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaManager : MonoBehaviour
{
    public MeshRenderer pantallaRenderer;
    public Material[] imagenesTutorial;
    public Material[] imagenesJuego;

    private int currentIndex = 0;
    private bool esTutorial = true;

    public void SetModoJuego()
    {
        esTutorial = false;
        currentIndex = 0;
    }

    public void DisplaySpecificImage(int index)
    {
        Material[] materialesActuales = esTutorial ? imagenesTutorial : imagenesJuego;

        if (materialesActuales != null && index >= 0 && index < materialesActuales.Length)
        {
            pantallaRenderer.material = materialesActuales[index];
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango en DisplaySpecificImage.");
        }
    }

    // Muestra la imagen correspondiente al tipo de quesadilla actual
    public void MostrarTipoQuesadilla(string tipoQuesadilla)
    {
        int index = ObtenerIndiceDeQuesadilla(tipoQuesadilla);
        if (index != -1)
        {
            DisplaySpecificImage(index);
            Debug.Log("Mostrando imagen para tipo de quesadilla: " + tipoQuesadilla);
        }
    }

    // Retorna el índice específico de la imagen en el array de juego
    private int ObtenerIndiceDeQuesadilla(string tipoQuesadilla)
    {
        // Asumiendo que las imágenes están en el mismo orden que `tiposQuesadilla` en GameManager
        switch (tipoQuesadilla)
        {
            case "quesadilla_frijoles": return 7;
            case "quesadilla_frijoles_queso": return 8;
            case "quesadilla_pollo": return 9;
            case "quesadilla_pollo_frijoles": return 10;
            case "quesadilla_pollo_queso": return 11;
            case "quesadilla_queso": return 12;
            default: return -1;
        }
    }
}
