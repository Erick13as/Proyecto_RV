using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public PantallaManager pantallaManager; // Referencia al PantallaManager
    private int currentStep = 0; // Paso actual del tutorial

    // Definir los pasos del tutorial como un enum para mayor claridad
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

    // Almacena el estado de cada paso
    private Dictionary<TutorialSteps, bool> pasosCompletados = new Dictionary<TutorialSteps, bool>();

    private void Start()
    {
        // Inicializar todos los pasos como no completados
        foreach (TutorialSteps step in System.Enum.GetValues(typeof(TutorialSteps)))
        {
            pasosCompletados[step] = false;
        }
        
        // Iniciar el tutorial en el primer paso
        IniciarPaso(TutorialSteps.EncenderSarten);
    }

    // Método para iniciar cada paso y actualizar la pantalla
    public void IniciarPaso(TutorialSteps paso)
    {
        currentStep = (int)paso;
        pantallaManager.NextDisplay(); // Cambia la imagen de la pantalla al paso actual
        Debug.Log("Iniciando paso: " + paso);
    }

    // Método para marcar el paso como completado
    public void CompletarPaso(TutorialSteps paso)
    {
        if (paso == (TutorialSteps)currentStep && !pasosCompletados[paso])
        {
            pasosCompletados[paso] = true;
            AvanzarSiguientePaso();
        }
    }

    // Método para avanzar al siguiente paso
    private void AvanzarSiguientePaso()
    {
        if (currentStep < System.Enum.GetValues(typeof(TutorialSteps)).Length - 1)
        {
            currentStep++;
            IniciarPaso((TutorialSteps)currentStep); // Cambiar al siguiente paso
        }
        else
        {
            Debug.Log("Tutorial completo.");
        }
    }
}

/*

Integración con Otros Scripts

public class Sarten : MonoBehaviour
{
    public TutorialManager tutorialManager; // Referencia al TutorialManager

    private void Encender()
    {
        // Lógica para encender el sartén
        tutorialManager.CompletarPaso(TutorialManager.TutorialSteps.EncenderSarten); // Marca el paso como completado
    }
}

*/