using UnityEngine;

public class PantallaManager : MonoBehaviour
{
    public MeshRenderer pantallaRenderer; // MeshRenderer de la pantalla
    public Material[] imagenesTutorial; // Array de materiales para el tutorial
    public Material[] imagenesJuego; // Array de materiales para el juego
    private int currentIndex = 0;
    private bool esTutorial = true; // Determina si estamos en el tutorial o en el juego

    // Cambia entre tutorial y juego
    public void SetModoJuego()
    {
        esTutorial = false;
        currentIndex = 0; // Reinicia el índice para el juego
        pantallaRenderer.material = imagenesJuego[currentIndex];
    }

    // Cambia al siguiente material en el array correspondiente
    public void NextDisplay()
    {
        Material[] materialesActuales = esTutorial ? imagenesTutorial : imagenesJuego;

        if (currentIndex < materialesActuales.Length - 1)
        {
            currentIndex++;
            pantallaRenderer.material = materialesActuales[currentIndex];
        }
    }

    public void DisplaySpecificImage(int index)
    {
        Material[] materialesActuales = esTutorial ? imagenesTutorial : imagenesJuego;

        if (index >= 0 && index < materialesActuales.Length)
        {
            currentIndex = index;
            pantallaRenderer.material = materialesActuales[index];
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