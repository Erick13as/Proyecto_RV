using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaManager : MonoBehaviour
{
    public MeshRenderer pantallaRenderer;
    public Material[] imagenesTutorial;  // La portada será el primer elemento
    public Material[] imagenesJuego;

    private int currentIndex = 0;
    private bool esTutorial = true;

    private Dictionary<string, int> imagenesQuesadillas = new Dictionary<string, int>
    {
        { "quesadilla_completa", 1 },
        { "quesadilla_frijoles_queso", 2 },
        { "quesadilla_frijoles", 3 },
        { "quesadilla_pollo_frijoles", 4 },
        { "quesadilla_pollo_queso", 5 },
        { "quesadilla_pollo", 6 },
        { "quesadilla_queso", 7 }
    };

    public void IniciarTutorial()
    {
        esTutorial = true;
        DisplaySpecificImage(0); // Muestra la portada
    }

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

    public void MostrarTipoQuesadilla(string tipoQuesadilla)
    {
        if (imagenesQuesadillas.TryGetValue(tipoQuesadilla, out int index))
        {
            DisplaySpecificImage(index);
            Debug.Log("Mostrando imagen para tipo de quesadilla: " + tipoQuesadilla);
        }
        else
        {
            Debug.LogWarning("Tipo de quesadilla no encontrado en el diccionario: " + tipoQuesadilla);
        }
    }
}
