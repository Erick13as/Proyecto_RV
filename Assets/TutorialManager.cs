using System.Collections;
using UnityEngine;

// Este script gestiona el flujo del tutorial, controla los pasos y cambia la pantalla automáticamente para guiar al usuario a través de los pasos básicos.
public class TutorialManager : MonoBehaviour
{
    public PantallaManager pantallaManager;
    public QuesadillaMonitor quesadillaMonitor; // Nueva referencia al QuesadillaMonitor
    private int currentStep = 0;

    public enum TutorialSteps
    {
        EncenderSarten,
        PonerTortilla,
        PonerFrijoles,
        PonerQueso,
        PonerCarne,
        PonerMasQueso,
        CerrarQuesadilla,
        LlevarQuesadillaAlSarten,
        CocinarQuesadilla,
        VoltearQuesadilla,
        ServirQuesadilla
    }

    private void Start()
    {
        IniciarPaso((TutorialSteps)currentStep);
    }

    public void IniciarPaso(TutorialSteps paso)
    {
        currentStep = (int)paso;
        pantallaManager.DisplaySpecificImage(currentStep); 
        Debug.Log("Iniciando paso: " + paso);
    }

    public void CompletarPasoActual()
    {
        if (currentStep < System.Enum.GetValues(typeof(TutorialSteps)).Length - 1)
        {
            currentStep++;
            IniciarPaso((TutorialSteps)currentStep); 
        }
        else
        {
            Debug.Log("Tutorial completo.");
            TerminarTutorial(); // Llamada para finalizar el tutorial y cambiar al modo de juego
        }
    }

    private void TerminarTutorial()
    {
        quesadillaMonitor.esTutorial = false; // Cambia al modo de juego en QuesadillaMonitor
        Debug.Log("Cambiando a modo de juego.");
    }
}
