using UnityEngine;

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

    // Método para iniciar el estado de una nueva quesadilla
    public void IniciarNuevaQuesadilla()
    {
        ResetearQuesadilla();
        Debug.Log("Iniciando una nueva quesadilla en QuesadillaMonitor.");
    }

    public void AgregarTortilla() { tortillaAgregada = true; VerificarCompletado(); }
    public void AgregarFrijoles() { frijolAgregado = true; VerificarCompletado(); }
    public void AgregarQueso() { quesoAgregado = true; VerificarCompletado(); }
    public void AgregarPollo() { carneAgregada = true; VerificarCompletado(); } 

    public void CerrarQuesadilla() { quesadillaCerrada = true; VerificarCompletado(); }
    public void CocinarQuesadilla() { quesadillaCocinada = true; VerificarCompletado(); }

    private void VerificarCompletado()
    {
        if (tortillaAgregada && (frijolAgregado || quesoAgregado || carneAgregada) && quesadillaCerrada && quesadillaCocinada)
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

    public void QuesadillaQuemada()
    {
        if (!esTutorial)
        {
            gameManager.QuesadillaQuemada();
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
