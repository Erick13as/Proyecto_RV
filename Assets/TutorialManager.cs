using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public PantallaManager pantallaManager;
    public QuesadillaMonitor quesadillaMonitor;
    private int currentStep = 0;
    private int currentImageIndex = 0;

    private int[] imagesPerStep = { 3, 1, 1, 3, 3, 3, 3, 1, 3, 2, 3 };
    private bool isDisplayingStepImages = false;

    public enum TutorialSteps
    {
        AprenderManiobrarObjetos,
        EquipoCocina,
        ConocerReceta,
        PonerTortilla,
        PonerFrijoles,
        PonerQueso,
        PonerCarne,
        CerrarQuesadilla,
        QuesadillaAlSarten,
        CocinarQuesadilla,
        ServirQuesadilla
    }

    private void Start()
    {
        pantallaManager.IniciarTutorial(); // Mostrar portada y prepararse para el tutorial
        IniciarPaso((TutorialSteps)currentStep);
    }

    public void IniciarPaso(TutorialSteps paso)
    {
        currentStep = (int)paso;
        currentImageIndex = 0;
        StartCoroutine(DisplayStepImages());
    }

    private IEnumerator DisplayStepImages()
    {
        isDisplayingStepImages = true;
        int totalImages = imagesPerStep[currentStep];

        while (currentImageIndex < totalImages)
        {
            pantallaManager.DisplaySpecificImage(currentImageIndex + GetStartingIndexForStep(currentStep));
            currentImageIndex++;
            yield return new WaitForSeconds(2f);
        }

        isDisplayingStepImages = false;
        CompletarPasoActual();
    }

    public void CompletarPasoActual()
    {
        if (!isDisplayingStepImages && currentStep < System.Enum.GetValues(typeof(TutorialSteps)).Length - 1)
        {
            currentStep++;
            IniciarPaso((TutorialSteps)currentStep);
        }
        else
        {
            TerminarTutorial();
        }
    }

    private void TerminarTutorial()
    {
        quesadillaMonitor.esTutorial = false;
        FindObjectOfType<GameManager>().IniciarJuego(); // Cambia a modo de juego
        Debug.Log("Tutorial completo. Cambiando a modo de juego.");
    }

    private int GetStartingIndexForStep(int step)
    {
        int startingIndex = 0;
        for (int i = 0; i < step; i++)
        {
            startingIndex += imagesPerStep[i];
        }
        return startingIndex;
    }
}
