using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public PantallaManager pantallaManager;
    private int currentStep = 0;
    private int currentImageIndex = 0;

    // Lista de pasos y el número de imágenes que corresponden a cada paso
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
        IniciarPaso((TutorialSteps)currentStep);
    }

    // Método para iniciar cada paso y comenzar la secuencia de imágenes correspondientes
    public void IniciarPaso(TutorialSteps paso)
    {
        currentStep = (int)paso;
        currentImageIndex = 0;
        Debug.Log("Iniciando paso: " + paso);
        StartCoroutine(DisplayStepImages());
    }

    // Corutina para mostrar todas las imágenes de un paso en secuencia
    private IEnumerator DisplayStepImages()
    {
        isDisplayingStepImages = true;
        int totalImages = imagesPerStep[currentStep];

        while (currentImageIndex < totalImages)
        {
            pantallaManager.DisplaySpecificImage(currentImageIndex + GetStartingIndexForStep(currentStep));
            Debug.Log("Mostrando imagen: " + currentImageIndex + " del paso " + currentStep);
            currentImageIndex++;
            yield return new WaitForSeconds(2f); // Tiempo entre imágenes
        }

        isDisplayingStepImages = false;
        CompletarPasoActual();
    }

    // Método para avanzar al siguiente paso una vez que todas las imágenes del paso actual han sido mostradas
    public void CompletarPasoActual()
    {
        if (!isDisplayingStepImages && currentStep < System.Enum.GetValues(typeof(TutorialSteps)).Length - 1)
        {
            currentStep++;
            IniciarPaso((TutorialSteps)currentStep); 
        }
        else
        {
            Debug.Log("Tutorial completo.");
            // Aquí podrías añadir lógica para empezar el juego después del tutorial
        }
    }

    // Calcula el índice inicial de las imágenes en el array en función del paso
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
