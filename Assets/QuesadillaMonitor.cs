using UnityEngine;

// Este script realiza el seguimiento de los ingredientes y pasos necesarios para completar una quesadilla y notifica al TutorialManager o GameManager según corresponda.
// QuesadillaMonitor valida la preparación de quesadillas en ambas fases (tutorial y juego) y notifica al script correspondiente.

public class QuesadillaMonitor : MonoBehaviour
{
    public GameManager gameManager;
    public TutorialManager tutorialManager;
    public bool esTutorial = true; 

    private bool tortillaAgregada = false;
    private bool frijolAgregado = false;
    private bool quesoAgregado = false;
    private bool carneAgregada = false;
    private bool quesadillaCerrada = false;
    private bool quesadillaCocinada = false;

    public void AgregarTortilla() { tortillaAgregada = true; VerificarCompletado(); }
    public void AgregarFrijol() { if (tortillaAgregada) { frijolAgregado = true; VerificarCompletado(); } }
    public void AgregarQueso() { if (tortillaAgregada) { quesoAgregado = true; VerificarCompletado(); } }
    public void AgregarCarne() { if (tortillaAgregada) { carneAgregada = true; VerificarCompletado(); } }
    public void CerrarQuesadilla() { if (tortillaAgregada && (frijolAgregado || quesoAgregado || carneAgregada)) { quesadillaCerrada = true; VerificarCompletado(); } }
    public void CocinarQuesadilla() { if (quesadillaCerrada) { quesadillaCocinada = true; VerificarCompletado(); } }

    private void VerificarCompletado()
    {
        if (tortillaAgregada && quesadillaCerrada && quesadillaCocinada)
        {
            if (esTutorial)
            {
                tutorialManager.CompletarPasoActual();
            }
            else
            {
                gameManager.CompletarQuesadilla();
            }
            ResetearQuesadilla();
        }
    }

    private void ResetearQuesadilla()
    {
        tortillaAgregada = false;
        frijolAgregado = false;
        quesoAgregado = false;
        carneAgregada = false;
        quesadillaCerrada = false;
        quesadillaCocinada = false;
    }
}


/*

Implementar en los scripts:

public class SartenScript : MonoBehaviour
{
    public QuesadillaMonitor quesadillaMonitor;

    public void Cocinar()
    {
        // Lógica para cocinar la quesadilla
        quesadillaMonitor.CocinarQuesadilla(); // Marca la quesadilla como cocinada en el monitor
    }
}

*/